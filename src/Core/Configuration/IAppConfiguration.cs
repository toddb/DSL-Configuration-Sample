using System.Net;
using Core;
using log4net;

namespace Core.Configuration
{
    public interface IAppConfiguration
    {
        IConnector IncomingConnector { get; }
        IConnector OutgoingConnector { get; }
        NetworkCredential OutgoingCredentials { get; }
        WebProxy Proxy { get; }
        string BaseUrl { get; }
        ILog Logger { get; }
    }
}