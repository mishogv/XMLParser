namespace XMLConvertor.Web.Services;

public interface IXMLConvertorService
{
    public Task ConvertXmlToJsonAndSave(string xmlFilePath, string outputDirectory, byte[]? byteContent);
}