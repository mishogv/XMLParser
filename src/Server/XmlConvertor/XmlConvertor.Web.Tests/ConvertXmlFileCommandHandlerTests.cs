using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XmlConvertor.Web.Commands;
using XmlConvertor.Web.Exceptions;
using XmlConvertor.Web.Extensions;
using XmlConvertor.Web.Handlers;
using XmlConvertor.Web.Services;

namespace XmlConvertor.Web.Tests;

[TestClass]
    public class ConvertXmlFileCommandHandlerTests
    {
        [TestMethod]
        public async Task Handle_ValidFile_CallsServicesCorrectly()
        {
            // Arrange
            var fileName = "validFile.xml";
            var fileBytes = "Hello World from a Fake File"u8.ToArray();
            var xmlConvertorServiceMock = new Mock<IXmlConvertorService>();

            var fileServiceMock = new Mock<IFileService>();
            var file = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "Data", fileName);
            var handler = new ConvertXmlFileCommandHandler(xmlConvertorServiceMock.Object, fileServiceMock.Object);
            var command = new ConvertXmlFileCommand(fileName, file);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            xmlConvertorServiceMock.Verify(x => x.ConvertXmlToJson(fileBytes), Times.Once);
            fileServiceMock.Verify(x => x.WriteToFileAsync(fileName.ReplaceXmlExtensionWithJson(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow(null, "validFile.xml")]
        [DataRow(new byte[0], "validFile.xml")]
        [DataRow(new byte[] { 1, 2, 3 }, null)]
        [DataRow(new byte[] { 1, 2, 3 }, "invalidFile.txt")]
        public void Handle_InvalidFile_ThrowsBusinessServiceException(byte[] fileContent, string fileName)
        {
            // Arrange
            var xmlConvertorServiceMock = new Mock<IXmlConvertorService>();
            var fileServiceMock = new Mock<IFileService>();

            var handler = new ConvertXmlFileCommandHandler(xmlConvertorServiceMock.Object, fileServiceMock.Object);
            var command = new ConvertXmlFileCommand(fileName, new Mock<IFormFile>().Object);


            // Act & Assert
            Assert.ThrowsExceptionAsync<CommandHandlerException>(() => handler.Handle(command, CancellationToken.None));
        }
    }