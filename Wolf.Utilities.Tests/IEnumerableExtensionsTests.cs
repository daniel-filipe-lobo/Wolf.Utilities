namespace Wolf.Utilities.Tests;

public class IEnumerableExtensionsTests
{
	[Fact]
	public void EmptyIfNull_ReturnsEmpty()
	{
		//arrange
		List<int>? list = null;

		//act
		var emptyList = list.EmptyIfNull();

		//assert
		emptyList.Should().BeEmpty();
	}

	[Fact]
	public void NullIfIsEmpty_ReturnsNull()
	{
		//arrange
		List<int>? list = [];

		//act
		var nullList = list.NullIfIsEmpty();

		//assert
		nullList.Should().BeNull();
	}

	[Fact]
	public void NullIfIsEmpty_ReturnsEmpty()
	{
		//arrange
		List<int>? list = [1];

		//act
		var result = list.NullIfIsEmpty();

		//assert
		result.Should().BeSameAs(list);
	}
}
