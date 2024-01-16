namespace XMLConvertor.Web.Exceptions;

public class BusinessServiceException : Exception
{
    public BusinessServiceException(string message)
        : base(message)
    {
    }

    public BusinessServiceException(string message, string? parameterName)
        : base(message) => this.ParameterName = parameterName;

    public string? ParameterName { get; set; }
}