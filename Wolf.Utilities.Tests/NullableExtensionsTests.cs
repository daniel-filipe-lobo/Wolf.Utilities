namespace Wolf.Utilities.Tests;

public class NullableExtensionsTests
{
	[Fact]
	public void ThrowIfIsNullOrWhiteSpace_ThrowExceptionForNull()
	{
		//arrange
		string? value = null;

		//act
		var action = () => value.ThrowIfIsNullOrWhiteSpace();

		//assert
		action.Should().Throw<Exception>();
	}

	[Fact]
	public void ThrowIfIsNullOrWhiteSpace_ThrowExceptionForEmpty()
	{
		//arrange
		string? value = "";

		//act
		var action = () => value.ThrowIfIsNullOrWhiteSpace();

		//assert
		action.Should().Throw<Exception>();
	}

	[Fact]
	public void ThrowIfIsNullOrWhiteSpace_NotThrowException()
	{
		//arrange
		string? value = "test";

		//act
		var action = () => value.ThrowIfIsNullOrWhiteSpace();

		//assert
		action.Should().NotThrow<Exception>();
	}

	[Fact]
	public void ThrowIfIsNull_ThrowException()
	{
		//arrange
		DateTimeOffset? value = null;

		//act
		var action = () => value.ThrowIfIsNull();

		//assert
		action.Should().Throw<Exception>();
	}

	[Fact]
	public void ThrowIfIsNull_NotThrowException()
	{
		//arrange
		int? value = 1;

		//act
		var action = () => value.ThrowIfIsNull();

		//assert
		action.Should().NotThrow<Exception>();
	}

	[Fact]
	public void ThrowIfIsNullForClass_ThrowException()
	{
		//arrange
		StringBuilder? value = null;

		//act
		var action = () => value.ThrowIfIsNull();

		//assert
		action.Should().Throw<Exception>();
	}

	[Fact]
	public void ThrowIfIsNullForClass_NotThrowException()
	{
		//arrange
		StringBuilder? value = new StringBuilder();

		//act
		var action = () => value.ThrowIfIsNull();

		//assert
		action.Should().NotThrow<Exception>();
	}

	[Fact]
	public void ThrowIfIsNullOrElementIsNull_ThrowExceptionForNull()
	{
		//arrange
		List<StringBuilder>? value = null;

		//act
		var action = () => value.ThrowIfIsNullOrElementIsNull();

		//assert
		action.Should().Throw<Exception>();
	}

	[Fact]
	public void ThrowIfIsNullOrElementIsNull_ThrowExceptionForNullElement()
	{
		//arrange
		List<StringBuilder?>? value = [null];

		//act
		var action = () => value.ThrowIfIsNullOrElementIsNull();

		//assert
		action.Should().Throw<Exception>();
	}

	[Fact]
	public void ThrowIfIsNullOrElementIsNull_NotThrowException()
	{
		//arrange
		List<StringBuilder>? value = [];

		//act
		var action = () => value.ThrowIfIsNullOrElementIsNull();

		//assert
		action.Should().NotThrow<Exception>();
	}
}
