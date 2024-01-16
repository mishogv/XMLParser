using System.Text.Json;
using static XMLConvertor.Web.Constants.ServerConstants;

namespace XMLConvertor.Web.Extensions;

public static class HttpExtensions
{
    public static Task WriteJson<T>(this HttpResponse response, T? obj)
    {
        response.ContentType = ApplicationJson;

        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        return response.WriteAsync(JsonSerializer.Serialize(obj));
    }
}