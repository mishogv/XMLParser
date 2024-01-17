using MediatR;

namespace XmlConvertor.Web.Commands;

public class ConvertXmlFileCommand : IRequest
{
    public ConvertXmlFileCommand(string name, IFormFile? file)
    {
        Name = name;
        File = file;
    }

    public string Name { get; set; }

    public IFormFile? File { get; set; }   
}