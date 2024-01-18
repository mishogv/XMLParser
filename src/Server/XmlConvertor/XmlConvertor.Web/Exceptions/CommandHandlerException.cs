namespace XmlConvertor.Web.Exceptions;

public class CommandHandlerException : BusinessServiceException
{
    public CommandHandlerException(string message) : base(message)
    {
    }

    public CommandHandlerException(string message, string? parameterName) : base(message, parameterName)
    {
    }
}