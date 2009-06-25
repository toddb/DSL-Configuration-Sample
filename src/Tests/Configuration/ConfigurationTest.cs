using System.Collections;
using System.Configuration;
using Core;
using Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Configuration
{
    /// <summary>
    /// Summary description for ConfigurationTest
    /// </summary>
    [TestClass]
    public class ConfigurationTest
    {

        [TestMethod]
        public void A_pretend_start()
        {
            var configuration = AppConfigurator.New(cfg =>
                                                        {
                                                            cfg.BaseUrl("http://somewhere.com");
                                                            cfg.RunWithProxyFromDotNetConfig();
                                                            cfg.UseCredentialsFromDotNetConfig();
                                                        });

            Assert.AreEqual("http://somewhere.com", configuration.BaseUrl);
        }

        [TestMethod]
        public void ReadingFromDotNetConfigViaLambda()
        {
            var configuration = AppConfigurator.New(cfg => cfg.BaseUrlFromDotNetConfig());

            Assert.AreEqual(ConfigurationManager.AppSettings["BaseURL"], configuration.BaseUrl);
        }

        [TestMethod]
        public void CanMockFleetConnector()
        {
            var connector = new Mock<IConnector>();
            connector.Setup(x => x.Fetch("")).Returns(new Hashtable());

            var ctx = Application.New(new AppConfiguration
                                          {
                                              IncomingConnector = connector.Object,
                                              OutgoingConnector = null,
                                              BaseUrl = "http://example.com",
                                          });

            ctx.Configuration.IncomingConnector.Fetch("");
            connector.Verify(x => x.Fetch(""), Times.Exactly(1));

        }

    }
}