namespace XMLConvertor.Web.Models;

public class ConvertXMLFileRequestModel
{
    public string Name { get; set; }

    public IFormFile? File { get; set; }
}