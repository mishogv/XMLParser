using System.Xml;
using Newtonsoft.Json;
using XmlConvertor.Web.Exceptions;

namespace XmlConvertor.Web.Services;

public class XmlConvertorService : IXmlConvertorService
{
    public async Task<string> ConvertXmlToJson(byte[]? byteContent)
    {
        if (byteContent is null)
        {
            throw new BusinessServiceException("The file content is empty!");
        }

        return await Task.Run(() =>
        {
            var doc = this.LoadXmlDocument(byteContent!);
            return this.SerializeXmlDocumentToJson(doc);
        });
    }

    private string SerializeXmlDocumentToJson(XmlDocument doc)
    {
        try
        {
            return JsonConvert.SerializeXmlNode(doc);
        }
        catch (Exception)
        {
            throw new BusinessServiceException("There was parsing error in the xml. Please validate the file before using!");
        }
    }

    private XmlDocument LoadXmlDocument(byte[] byteContent)
    {
        var doc = new XmlDocument();
        using var ms = new MemoryStream(byteContent);

        try
        {
            doc.Load(ms);
        }
        catch (Exception)
        {
            throw new BusinessServiceException("There was parsing error while processing the xml. Please validate the file before upload!");
        }

        return doc;
    }
}