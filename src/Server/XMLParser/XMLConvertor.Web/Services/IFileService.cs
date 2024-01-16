namespace XMLConvertor.Web.Services;

public interface IFileService
{
    public Task WriteToFileAsync(string fileName, string content);
}