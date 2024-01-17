using MediatR;
using Microsoft.AspNetCore.Mvc;
using XmlConvertor.Web.Commands;
using XmlConvertor.Web.Extensions;
using XmlConvertor.Web.Models;
using static XmlConvertor.Web.Constants.ServerConstants;

namespace XmlConvertor.Web.Features;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces(ApplicationJson)]
public class XmlConvertorController : ControllerBase
{
    private readonly IMediator mediator;

    public XmlConvertorController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    /// <summary>
    /// Upload Xml file for converting it to Json.
    /// </summary>
    /// <param name="model">The XmlRequest model containing the content of the file and it's name.</param>
    /// <returns>Success status code.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConvertXmlFile([FromForm] ConvertXmlFileRequestModel model)
        => await this.mediator.Send(new ConvertXmlFileCommand(model.Name, model.File)) 
            .ToOkResult();
}