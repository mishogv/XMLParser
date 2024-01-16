using Microsoft.AspNetCore.Mvc;

namespace XMLConvertor.Web.Extensions;

public static class TaskExtensions
{
    public static async Task<OkObjectResult> ToOkResult<T>(this Task<T> task)
        => new (await task);

    public static async Task<OkResult> ToOkResult(this Task task)
    {
        await task;
        return new OkResult();
    }
}