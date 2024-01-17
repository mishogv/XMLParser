namespace XmlConvertor.Web.Services;

public interface IXmlConvertorService
{
    public Task ConvertXmlToJsonAndSave(byte[]? byteContent, string fileName);
}