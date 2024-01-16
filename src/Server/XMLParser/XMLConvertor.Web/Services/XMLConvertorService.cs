using System.Xml;
using Newtonsoft.Json;
using XMLConvertor.Web.Exceptions;

namespace XMLConvertor.Web.Services;

public class XMLConvertorService : IXMLConvertorService
{
    private readonly IFileService fileService;

    public XMLConvertorService(IFileService fileService)
    {
        this.fileService = fileService;
    }
    
    public async Task ConvertXmlToJsonAndSave(byte[]? byteContent, string fileName)
    {
        if (byteContent == null || string.IsNullOrWhiteSpace(fileName) || !fileName.EndsWith(".xml"))
        {
            throw new BusinessServiceException("The file is empty or name is not specified or the file is with invalid extension!");
        }
        
        var doc = new XmlDocument();
        using var ms = new MemoryStream(byteContent);

        try
        {
            doc.Load(ms);
        }
        catch (Exception _)
        {
            throw new BusinessServiceException("There was parsing error while processing the xml. Please validate the file before upload!");
        }

        string json;

        try
        {
            json = JsonConvert.SerializeXmlNode(doc);
        }
        catch (Exception _)
        {
            throw new BusinessServiceException("There was parsing error in the xml. Please validate the file before using!");
        }

        await this.fileService.WriteToFileAsync(fileName, json);
    }
}