using System.Text;
using XMLConvertor.Web.Exceptions;

namespace XMLConvertor.Web.Services;

public class FileService : IFileService
{
    public async Task WriteToFileAsync(string fileName, string content)
    {
        try
        {
            await using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            await using var writer = new StreamWriter(fileStream, Encoding.UTF8);
            await writer.WriteAsync(content);
        }
        catch (Exception _)
        {
            throw new BusinessServiceException("The file system of the server is busy.");
        }
    }
}