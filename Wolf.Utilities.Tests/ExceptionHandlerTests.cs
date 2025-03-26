namespace Wolf.Utilities.Tests
{
	public class ExceptionHandlerTests
	{
		internal class ExceptionFactoryImplementation : ExceptionFactory
		{
			public override Exception Create(string? message, Exception innerException)
			{
				return new Exception(message, innerException);
			}
		}

		[Fact]
		public void Create_ReturnsExceptionWithParameters()
		{
			//arrange
			var parameter = "{D2D02427-6F77-4E14-93D7-54883272E299}";
			var value = "{223E644E-2E12-4C5B-AFA8-52DB487A2200}";

			//act
			var exception = ExceptionHandler<ExceptionFactoryImplementation>.Create(Mock.Of<ILogger>(), new Exception(), (parameter, value));

			//assert
			exception.Message.Should().ContainAll(parameter, value);
		}

		[Fact]
		public void CreateWithMessge_ReturnsExceptionWithParameters()
		{
			//arrange
			var parameter = "{DEF8833E-558C-4555-9120-3F0BD2883CAE}";
			var value = "{934223D6-FDE2-4CDA-A5DE-BA5609089532}";
			var message = "{56733F1C-EF21-4BE8-94AA-14F9698D8552}";

			//act
			var exception = ExceptionHandler<ExceptionFactoryImplementation>.Create(Mock.Of<ILogger>(), message, new Exception(), (parameter, value));

			//assert
			exception.Message.Should().ContainAll(message, parameter, value);
		}
	}
}
