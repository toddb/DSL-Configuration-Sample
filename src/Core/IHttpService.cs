using System.Xml;

namespace Core
{
    public interface IHttpService
    {
        XmlDocument Execute();
        string Body { get; set; }
        HttpTypeEnum Method { get; set; }
        string Url { get; set; }
        void PostString(string urlBase, string id, string postString);
    }

    public enum HttpTypeEnum
    {
        Get,
        Post
    }

}