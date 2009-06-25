using System.Collections;
using System.Collections.Generic;
using Core.Configuration;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Models;
using Moq;

namespace Tests.Core
{
    [TestClass]
    public class ApplicationTest
    {
        private Mock<IConnector> _mockConnector;
        private Mock<IAssetService> _mockAssetService;
        private Application _ctx;
        private Mock<ILog> _mockLogger;

        [TestInitialize]
        public void Initialize()
        {
            _mockLogger = new Mock<ILog>();
            _mockLogger.Setup(x => x.IsWarnEnabled).Returns(true);
            // emulate the configurator because the logservice is static
            LogService.log = _mockLogger.Object;

            _mockConnector = new Mock<IConnector>();
            _mockConnector.Setup(x => x.Fetch(It.IsAny<string>())).Returns(new Hashtable());
            _mockConnector.Setup(x => x.Send((It.IsAny<List<Asset>>())));
            

            _mockAssetService = new Mock<IAssetService>();
            _mockAssetService.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<List<Asset>>()));
            _mockAssetService.Setup(x => x.BuildAssets(It.IsAny<Hashtable>())).Returns(new List<Asset>());

            _ctx = Application.New(new AppConfiguration
                                       {
                                           IncomingConnector = _mockConnector.Object,
                                           OutgoingConnector = _mockConnector.Object,
                                           AssetService = _mockAssetService.Object,
                                           BaseUrl = "http://example.com",
                                           Logger = _mockLogger.Object
                                       });

            ApplicationRunner.Run(_ctx);
        }

        [TestMethod]
        public void CallsGet()
        {
            _mockConnector.Verify(x => x.Fetch(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void CallsProcess()
        {
            _mockAssetService.Verify(x => x.BuildAssets(It.IsAny<Hashtable>()), Times.Exactly(1));
            Assert.AreEqual(new Hashtable().Count, _ctx.Assets.Count);
        }

        [TestMethod]
        public void CallsPost()
        {
            _mockAssetService.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<List<Asset>>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ThrowNotificationExceptionIfCadburyListDoesNotLookToBeFiltered()
        {
            _mockLogger.Verify(x => x.Info(It.IsAny<object>()), Times.Exactly(1));
        }
       
    }
}