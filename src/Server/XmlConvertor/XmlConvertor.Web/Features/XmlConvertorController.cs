using Microsoft.AspNetCore.Mvc;
using XmlConvertor.Web.Extensions;
using XmlConvertor.Web.Models;
using XmlConvertor.Web.Services;
using static XmlConvertor.Web.Constants.ServerConstants;

namespace XmlConvertor.Web.Features;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces(ApplicationJson)]
public class XmlConvertorController : ControllerBase
{
    private readonly IXmlConvertorService xmlConvertorService;

    public XmlConvertorController(IXmlConvertorService xmlConvertorService)
    {
        this.xmlConvertorService = xmlConvertorService;
    }
    
    /// <summary>
    /// Upload Xml file for converting it to Json.
    /// </summary>
    /// <param name="model">The XmlRequest model containing the content of the file and it's name.</param>
    /// <returns>Success status code.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConvertXmlFile([FromForm] ConvertXmlFileRequestModel model)
        => await this.xmlConvertorService
            .ConvertXmlToJsonAndSave(model.File?.GetBytes(), model.Name)
            .ToOkResult();
}