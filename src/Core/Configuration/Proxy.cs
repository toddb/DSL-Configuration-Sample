using System;
using System.Configuration;
using System.Net;

namespace Core.Configuration
{
    public class Proxy
    {

        public static WebProxy DotNetConfig
        {
            get
            {
                WebProxy proxy = null;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Proxy.Url"]))
                {
                    proxy = new WebProxy(ConfigurationManager.AppSettings["Proxy.Url"], true)
                                {
                                    Credentials = new NetworkCredential(
                                        ConfigurationManager.AppSettings["Proxy.Username"],
                                        ConfigurationManager.AppSettings["Proxy.Password"],
                                        ConfigurationManager.AppSettings["Proxy.Domain"])
                                };
                }
                return proxy;
            }
        }
        public static WebProxy Custom(string username, string password, string domain, string url)
        {
            return new WebProxy(username, true)
                       {
                           Credentials = new NetworkCredential(username, password, domain)
                       };
        }

    }
}