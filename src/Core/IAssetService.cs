using System.Collections;
using System.Collections.Generic;
using Models;

namespace Core
{
    public interface IAssetService
    {
        IHttpService HttpService { get; set; }
        void Send(string urlBase, List<Asset> assets);
        List<Asset> BuildAssets(Hashtable assetIds);
    }
}
