namespace Wolf.Utilities;

public interface IExceptionFactory
{
	public Exception Create(string? message, Exception innerException);
}
