using System;
using System.Configuration;
using System.Net;
using log4net;

namespace Core.Configuration
{
    public class AppConfigurator : IAppConfigurator
    {
        private NetworkCredential _credentials;
        private WebProxy _proxy;
        private string _url;
        private ILog _logger;
        private IConnector _incoming;
        private IConnector _outgoing;

        AppConfiguration Create()
        {
            var cfg = new AppConfiguration
                          {
                              IncomingConnector = _incoming,
                              OutgoingConnector = _outgoing,
                              Proxy = _proxy,
                              OutgoingCredentials = _credentials,
                              BaseUrl = _url,
                              Logger = _logger
                          };
            return cfg;
        }

        public static AppConfiguration New(Action<IAppConfigurator> action)
        {
            using (var configurator = new AppConfigurator())
            {
                action(configurator);
                return configurator.Create();
            }
        }

        public void IncomingConnector(IConnector connector)
        {
            _incoming = connector;
        }

        public void OutgoingConnector(IConnector connector)
        {
            _outgoing = connector;
        }

        public void BaseUrl(string url)
        {
            _url = url;
        }

        public void BaseUrlFromDotNetConfig()
        {
            _url = ConfigurationManager.AppSettings[KnownDotNetConfig.BaseUrl];
        }

        public void UseCredentials(string username, string password)
        {
            _credentials = Credentials.Custom(username, password);
        }

        public void UseCredentialsFromDotNetConfig()
        {
            _credentials = Credentials.DotNetConfig;
        }

        public void RunWithNoProxy()
        {
            _proxy = null;
        }

        public void RunWithProxyFromDotNetConfig()
        {
            _proxy = Proxy.DotNetConfig;
        }

        public void RunWithProxyAs(string username, string password, string domain, string url)
        {
            _proxy = Proxy.Custom(username, password, domain, url);
        }

        public void UseLog4Net()
        {
            _logger = LogService.log;
        }

        public void UseLoggerCustom(ILog logService)
        {
            _logger = logService;
        }

        public void Dispose()
        {
            
        }
    }
}