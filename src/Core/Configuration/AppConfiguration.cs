using System.Net;
using Core;
using log4net;

namespace Core.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public IAssetService AssetService { get; set;}
        public IConnector IncomingConnector { get; set; }
        public IConnector OutgoingConnector { get; set; }
        public NetworkCredential OutgoingCredentials { get; set; }
        public WebProxy Proxy { get; set;}
        public string BaseUrl { get; set; }
        public ILog Logger { get; set; }
    }
}