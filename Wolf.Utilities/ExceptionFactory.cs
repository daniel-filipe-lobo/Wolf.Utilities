namespace Wolf.Utilities
{
	public abstract class ExceptionFactory : IExceptionFactory 
	{
		public abstract Exception Create(string? message, Exception innerException);
	}
}
