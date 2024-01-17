using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XmlConvertor.Web.Services;

namespace XmlConvertor.Web.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XmlConvertor.Web.Exceptions;

[TestClass]
public class XmlConvertorServiceTests
{
    [TestMethod]
    public async Task ConvertXmlToJson_ValidXml_ReturnsJson()
    {
        // Arrange
        var xmlConvertorService = new XmlConvertorService();
        var xmlContent = "<root><element>value</element></root>";
        var byteContent = Encoding.UTF8.GetBytes(xmlContent);

        // Act
        var result = await xmlConvertorService.ConvertXmlToJson(byteContent);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(IsJson(result));
    }

    [TestMethod]
    public async Task ConvertXmlToJson_InvalidXml_ThrowsBusinessServiceException()
    {
        // Arrange
        var xmlConvertorService = new XmlConvertorService();
        var invalidXmlContent = "<root><element>value</root>";
        var byteContent = Encoding.UTF8.GetBytes(invalidXmlContent);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<BusinessServiceException>(() =>
            xmlConvertorService.ConvertXmlToJson(byteContent));
    }

    [TestMethod]
    public async Task ConvertXmlToJson_NullByteContent_ThrowsArgumentNullException()
    {
        // Arrange
        var xmlConvertorService = new XmlConvertorService();

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            xmlConvertorService.ConvertXmlToJson(null));
    }
    
    private bool IsJson(string json)
    {
        try
        {
            JToken.Parse(json);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}