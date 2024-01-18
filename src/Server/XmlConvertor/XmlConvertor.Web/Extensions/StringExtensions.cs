namespace XmlConvertor.Web.Extensions;

public static class StringExtensions
{
    public static string ReplaceXmlExtensionWithJson(this string value)
    {
        return value.Replace(".xml", ".json");
    }
}