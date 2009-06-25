using System.Collections;
using System.Collections.Generic;
using Core.Configuration;
using Models;

namespace Core
{
    public class Application : IApplication
    {
        public AppConfiguration Configuration { get; private set; }
        public List<Asset> Assets { get; private set; }
        public Hashtable AssetIds { get; private set; }

        public static Application New (AppConfiguration cfg)
        {
            return new Application(cfg);
        }

        Application(AppConfiguration cfg)
        {
            Configuration = cfg;
        }

        public void Get()
        {
            AssetIds = Configuration.IncomingConnector.Fetch(Configuration.BaseUrl);
        }

        public void Process()
        {
            Assets = Configuration.AssetService.BuildAssets(AssetIds);
            Configuration.Logger.Info("Processing");
        }

        public void Post()
        {
            Configuration.AssetService.Send(Configuration.BaseUrl, Assets);
        }
    }
}