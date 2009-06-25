using System;
using Core;
using Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Acceptance
{
    [TestClass]
    public class ApplicationIntegrationTest
    {

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestApplicationRunnerAsIntegrationTest()
        {
            ApplicationRunner.Run(Application.New(AppConfigurator.New(cfg =>
                                                                          {
                                                                              cfg.BaseUrlFromDotNetConfig();
                                                                              cfg.RunWithProxyFromDotNetConfig();
                                                                              cfg.UseCredentialsFromDotNetConfig();
                                                                              cfg.UseLog4Net();
                                                                          })));


        }

    }
}