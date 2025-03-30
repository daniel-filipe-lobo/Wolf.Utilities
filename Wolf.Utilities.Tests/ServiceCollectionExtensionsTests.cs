namespace Wolf.Utilities.Tests
{
	public class ServiceCollectionExtensionsTests
	{
		[Fact]
		public void AddLogger_ContainsLogger()
		{
			//arrange
			var services = new ServiceCollection();

			//act
			services.AddLogger();

			//assert
			services.Should().Contain(serviceDescriptor => serviceDescriptor.ServiceType == typeof(ILoggerFactory));
			services.Should().Contain(serviceDescriptor => serviceDescriptor.ServiceType == typeof(ILogger<>));
		}
	}
}
