using MediatR;
using XmlConvertor.Web.Commands;
using XmlConvertor.Web.Exceptions;
using XmlConvertor.Web.Extensions;
using XmlConvertor.Web.Services;

namespace XmlConvertor.Web.Handlers;

public class ConvertXmlFileCommandHandler : IRequestHandler<ConvertXmlFileCommand>
{
    private readonly IXmlConvertorService xmlConvertorService;
    private readonly IFileService fileService;

    public ConvertXmlFileCommandHandler(IXmlConvertorService xmlConvertorService, IFileService fileService)
    {
        this.xmlConvertorService = xmlConvertorService;
        this.fileService = fileService;
    }
    
    public async Task Handle(ConvertXmlFileCommand request, CancellationToken cancellationToken)
    {
        var fileBytes = request.File?.GetBytes();
        
        this.ValidateByteContentAndFileName(fileBytes, request.Name);
        var json = await this.xmlConvertorService.ConvertXmlToJson(fileBytes);
        
        await this.fileService.WriteToFileAsync(request.Name.ReplaceXmlExtensionWithJson(), json);
    }
    
    private void ValidateByteContentAndFileName(byte[]? byteContent, string fileName)
    {
        if (byteContent == null || byteContent?.Length == 0 || string.IsNullOrWhiteSpace(fileName) || !fileName.EndsWith(".xml"))
        {
            throw new CommandHandlerException("The file is empty or name is not specified or the file is with invalid extension!");
        }
    }
}