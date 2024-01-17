using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using XmlConvertor.Web.Exceptions;
using XmlConvertor.Web.Extensions;
using static XmlConvertor.Web.Constants.ServerConstants;

namespace XmlConvertor.Web.Middlewares;

public class ExceptionMiddleware
{
    private const string StatusCodePropertyName = "StatusCode";
    private const string ExceptionAdditionalData = "Data";

    public RequestDelegate Get =>
        async httpContext =>
        {
            var errorFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = errorFeature
                ?.Error ?? new Exception($"Cannot get exception from {nameof(IExceptionHandlerFeature)}");

            var instanceId = Guid.NewGuid();
            var problemDetails = new ProblemDetails { Instance = instanceId.ToString() };

            switch (exception)
            {
                case BadHttpRequestException badHttpRequestException:
                    this.HandleBadHttpRequest(problemDetails, badHttpRequestException);
                    break;
                case BusinessServiceException businessException:
                    this.HandleValidationException(problemDetails, businessException);
                    break;
                default:
                    this.HandleException(problemDetails);
                    break;
            }

            httpContext.Response.StatusCode = problemDetails.Status ?? 500;
            await httpContext.Response.WriteJson(problemDetails);
        };

    private void HandleBadHttpRequest(
        ProblemDetails problemDetails,
        BadHttpRequestException exception)
    {
        var status = (int?)typeof(BadHttpRequestException)
            .GetProperty(
                StatusCodePropertyName,
                BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(exception);

        problemDetails.Title = BadHttpRequestExceptionTitle;
        problemDetails.Status = status;
    }

    private void HandleValidationException(
        ProblemDetails problemDetails,
        BusinessServiceException exception)
    {
        problemDetails.Title = ValidationExceptionTitle;
        problemDetails.Detail = exception.Message;
        problemDetails.Status = 422;
        problemDetails.Extensions[ExceptionAdditionalData] = exception.ParameterName;
    }

    private void HandleException(
        ProblemDetails problemDetails)
    {
        problemDetails.Title = UnhandledExceptionTitle;
        problemDetails.Status = (int)HttpStatusCode.InternalServerError;
    }
}