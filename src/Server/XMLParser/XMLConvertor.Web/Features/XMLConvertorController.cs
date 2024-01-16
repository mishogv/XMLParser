using Microsoft.AspNetCore.Mvc;
using XMLConvertor.Web.Extensions;
using XMLConvertor.Web.Models;
using XMLConvertor.Web.Services;
using static XMLConvertor.Web.Constants.ServerConstants;

namespace XMLConvertor.Web.Features;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces(ApplicationJson)]
public class XMLConvertorController : ControllerBase
{
    private readonly IXMLConvertorService XMLConvertorService;

    public XMLConvertorController(IXMLConvertorService XMLConvertorService)
    {
        this.XMLConvertorService = XMLConvertorService;
    }
    
    /// <summary>
    /// Upload XML file for converting it to Json.
    /// </summary>
    /// <param name="model">The XMLRequest model containing the content of the file and it's name.</param>
    /// <returns>Success status code.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConvertXMLFile([FromForm] ConvertXMLFileRequestModel model)
        => await this.XMLConvertorService
            .ConvertXmlToJsonAndSave(model.Name, model.Content)
            .ToOkResult();
}