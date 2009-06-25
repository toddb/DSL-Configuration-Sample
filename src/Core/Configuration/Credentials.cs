using System.Configuration;
using System.Net;

namespace Core.Configuration
{
    public class Credentials
    {
        public static NetworkCredential DotNetConfig
        {
            get
            {
                return new NetworkCredential(ConfigurationManager.AppSettings["Asset.Username"],
                                             ConfigurationManager.AppSettings["Asset.Password"]);
            }
        }

        public static NetworkCredential Custom(string username, string password)
        {
            return new NetworkCredential(username, password);
        }

    }
}