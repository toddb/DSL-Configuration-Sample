using System;
using Core;
using log4net;

namespace Core.Configuration
{
    public interface IAppConfigurator : IDisposable
    {
        void IncomingConnector(IConnector connector);
        void OutgoingConnector(IConnector connector);
        void BaseUrl(string url);
        void BaseUrlFromDotNetConfig();
        void UseCredentials(string username, string password);
        void UseCredentialsFromDotNetConfig();
        void RunWithNoProxy();
        void RunWithProxyFromDotNetConfig();
        void RunWithProxyAs(string username, string password, string domain, string url);
        void UseLog4Net();
        void UseLoggerCustom(ILog logService);
    }
}