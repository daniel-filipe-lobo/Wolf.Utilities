namespace Wolf.Utilities;

public static class ExceptionHandler<TExceptionFactory>
	where TExceptionFactory : ExceptionFactory, new()
{
	public static Exception Create(ILogger logger, string? message, Exception exception, params (string ParameterName, object? ParameterValue)[] parametersInformation)
	{
		var parameterMessage = Parameters.Format(parametersInformation);
		var completeMessage = message;
		if (string.IsNullOrWhiteSpace(completeMessage))
		{
			completeMessage = parameterMessage;
		}
		else
		{
			completeMessage += $"\r\n{parameterMessage}";
		}
		var exceptionFactory = new TExceptionFactory();
		var tException = exceptionFactory.Create(completeMessage, exception);
		logger.LogError(tException, completeMessage);
		return exception;
	}

	public static Exception Create(ILogger logger, Exception exception, params (string ParameterName, object? ParameterValue)[] parametersInformation)
	{
		return Create(logger, null, exception, parametersInformation);
	}
}
