namespace XMLConvertor.Web.Services;

public interface IXMLConvertorService
{
    public Task ConvertXmlToJsonAndSave(byte[]? byteContent, string fileName);
}