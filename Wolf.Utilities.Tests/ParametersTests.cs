namespace Wolf.Utilities.Tests;

public class ParametersTests
{
	[Fact]
	public void Format_ReturnsJson()
	{
		//arrange
		string parameter = "{412EBE02-75C0-4438-9B97-64AB09F146F5}";
		for (int index = 0; index < 5; ++index) { parameter = parameter + parameter; }
		var parameterInformation = (nameof(parameter), parameter);

		//act
		var result = Parameters.Format([parameterInformation]);

		//assert
		result.Should().StartWith("Parameters: parameter = \"");
		result.Should().EndWith("...");
	}
}
