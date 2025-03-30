namespace Wolf.Utilities
{
	public static class ServiceCollectionExtensions
	{
		public static void AddLogger(this IServiceCollection serviceDescriptors)
		{
			serviceDescriptors.TryAddSingleton<ILoggerFactory, LoggerFactory>();
			serviceDescriptors.TryAddSingleton(typeof(ILogger<>), typeof(Logger<>));
		}
	}
}
