namespace XmlConvertor.Web.Services;

public interface IXmlConvertorService
{
    public Task<string> ConvertXmlToJson(byte[]? byteContent);
}