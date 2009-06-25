using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Models;

namespace Core
{
    public interface IConnector
    {
        Hashtable Fetch(string urlBase);
        XmlDocument ToXml(List<Asset > assets);
        void Send(List<Asset> assets);
    }
}