using System.Collections.Generic;

namespace langchain.Schema
{
    public interface IDocumentParam
    {
        string PageContent { get; set; }
        Dictionary<string, object> Metdadata { get; set; }
    }
    public class Document : IDocumentParam
    {
        public string PageContent { get; set; }
        public Dictionary<string, object> Metdadata { get; set; }

        public Document(IDocumentParam fields = null)
        {
            PageContent = fields?.PageContent ?? PageContent;
            Metdadata = fields?.Metdadata ?? new Dictionary<string, object>();
        }
    }
}
