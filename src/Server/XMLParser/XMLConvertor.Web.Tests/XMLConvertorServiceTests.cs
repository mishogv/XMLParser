using System.Text;
using XMLConvertor.Web.Services;

namespace XMLConvertor.Web.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading.Tasks;
using XMLConvertor.Web.Exceptions;

[TestClass]
public class XMLConvertorServiceTests
{
    [TestMethod]
    public async Task ConvertXmlToJsonAndSave_ValidInput_Success()
    {
        // Arrange
        var xmlConvertorService = new XMLConvertorService(new FileService());
        var xmlContent = "<root><element>value</element></root>"u8.ToArray();
        var fileName = "test.xml";

        // Act
        await xmlConvertorService.ConvertXmlToJsonAndSave(xmlContent, fileName);

        // Assert
        Assert.IsTrue(File.Exists(fileName));
    }

    [TestMethod]
    public async Task ConvertXmlToJsonAndSave_InvalidFileName_ThrowsException()
    {
        // Arrange
        var xmlConvertorService = new XMLConvertorService(new FileService());
        var xmlContent = "<root><element>value</element></root>"u8.ToArray();
        var invalidFileName = "test.txt";

        // Act and Assert
        await Assert.ThrowsExceptionAsync<BusinessServiceException>(() =>
            xmlConvertorService.ConvertXmlToJsonAndSave(xmlContent, invalidFileName));
    }

    [TestMethod]
    public async Task ConvertXmlToJsonAndSave_InvalidXmlContent_ThrowsException()
    {
        // Arrange
        var xmlConvertorService = new XMLConvertorService(new FileService());
        var invalidXmlContent = "Invalid XML content"u8.ToArray();
        var fileName = "test.xml";

        // Act and Assert
        await Assert.ThrowsExceptionAsync<BusinessServiceException>(() =>
            xmlConvertorService.ConvertXmlToJsonAndSave(invalidXmlContent, fileName));
    }

    [TestMethod]
    public async Task ConvertXmlToJsonAndSave_FileSystemError_ThrowsException()
    {
        // Arrange
        var xmlContent = Encoding.UTF8.GetBytes("<root><element>value</element></root>");
        var fileName = "test.xml";

        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(f => f.WriteToFileAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new BusinessServiceException("File system error"));

        var xmlConvertorServiceWithMock = new XMLConvertorService(mockFileService.Object);

        // Act and Assert
        await Assert.ThrowsExceptionAsync<BusinessServiceException>(() =>
            xmlConvertorServiceWithMock.ConvertXmlToJsonAndSave(xmlContent, fileName));
    }
}