using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Moq;

namespace Tests.Core
{
    [TestClass]
    public class ApplicationRunnerTest
    {
        [TestMethod]
        public void UtilizationApplicationCallsGetProcessPost()
        {
            var mock = new Mock<IApplication>();

            ApplicationRunner.Run(mock.Object);

            mock.Verify(x => x.Get(), Times.Exactly(1));
            mock.Verify(x => x.Process(), Times.Exactly(1));
            mock.Verify(x => x.Post(), Times.Exactly(1));
        }

    }
}