namespace Wolf.Utilities.Tests;

public class StringExtensionsTests
{
	[Fact]
	public void EscapeBraces_ReturnsEscaped()
	{
		//arrange
		string text = "{}";

		//act
		var result = text.EscapeBraces();

		//assert
		result.Should().Be("{{}}");
	}

	[Fact]
	public void XpathEscape_ReturnsEscaped()
	{
		//arrange
		string text = @"'""\";

		//act
		var result = text.XpathEscape();

		//assert
		result.Should().Be(@"\\'\\""\\");
	}

	[Fact]
	public void RemoveDiacritics_ReturnsWithoutAccent()
	{
		//arrange
		string text = @"á";

		//act
		var result = text.RemoveDiacritics();

		//assert
		result.Should().Be(@"a");
	}

	[Fact]
	public void RemoveDiacritics_ReturnsSameString()
	{
		//arrange
		string text = @"a";

		//act
		var result = text.RemoveDiacritics();

		//assert
		result.Should().Be(@"a");
	}

	[Fact]
	public void HasDiacritics_ReturnsTrue()
	{
		//arrange
		string text = @"á";

		//act
		var result = text.HasDiacritics();

		//assert
		result.Should().BeTrue();
	}

	[Fact]
	public void HasDiacritics_ReturnsFalse()
	{
		//arrange
		string? text = null;

		//act
		var result = text.HasDiacritics();

		//assert
		result.Should().BeFalse();
	}

	[Fact]
	public void IsValidEmail_ReturnsTrue()
	{
		//arrange
		string? text = "a@a.pt";

		//act
		var result = text.IsValidEmail();

		//assert
		result.Should().BeTrue();
	}

	[Fact]
	public void IsValidEmail_ReturnsFalse()
	{
		//arrange
		string? text = "a@a.abcde";

		//act
		var result = text.IsValidEmail();

		//assert
		result.Should().BeFalse();
	}

	[Fact]
	public void IsValidEmail_ReturnsFalseForNull()
	{
		//arrange
		string? text = null;

		//act
		var result = text.IsValidEmail();

		//assert
		result.Should().BeFalse();
	}

	[Fact]
	public void HashDataSha512_ReturnsHash()
	{
		//arrange
		string? text = "a";

		//act
		var result = text.HashSha512ToBase64();

		//assert
		result.Should().Be("H0D8ktokFpR1CXnubPWC8tXX0o4YM13gWrxU0FYOD1MChgxlK/CNVgJSql50IQVG82n7u86MEs/HlXsmUv6adQ==");
	}

	[Fact]
	public void GetValidFileName_ReturnsValidName()
	{
		//arrange
		string? fileName = "file/name.txt";

		//act
		var result = fileName.GetValidFileName();

		//assert
		result.Should().Be("filename.txt");
	}

	[Fact]
	public void CamelCase_ReturnsCamelCase()
	{
		//arrange
		string? text = "CamelCasing";

		//act
		var result = text.CamelCase();

		//assert
		result.Should().Be("camelCasing");
	}

	[Fact]
	public void CamelCase_ReturnsEmpty()
	{
		//arrange
		string? text = "";

		//act
		var result = text.CamelCase();

		//assert
		result.Should().BeEmpty();
	}

	[Fact]
	public void CamelCase_ReturnsCamelCaseForOneLetter()
	{
		//arrange
		string? text = "C";

		//act
		var result = text.CamelCase();

		//assert
		result.Should().Be("c");
	}

	[Fact]
	public void RemoveExtraSpaces_ReturnsWithoutExtraSpaces()
	{
		//arrange
		string? text = "A  B";

		//act
		var result = text.RemoveExtraSpaces();

		//assert
		result.Should().Be("A B");
	}

	[Fact]
	public void HasExtraSpaces_ReturnsTrue()
	{
		//arrange
		string? text = "A  B";

		//act
		var result = text.HasExtraSpaces();

		//assert
		result.Should().BeTrue();
	}

	[Fact]
	public void ReplaceNonWordCharacters_ReturnsReplacedCharacteres()
	{
		//arrange
		string? text = "a.b.c";

		//act
		var result = text.ReplaceNonWordCharacters('0');

		//assert
		result.Should().Be("a0b0c");
	}

	[Fact]
	public void ReplaceNonWordCharacters_ReturnsRemovedControlCharacteres()
	{
		//arrange
		string? text = "a\nc";

		//act
		var result = text.ReplaceNonWordCharacters(null);

		//assert
		result.Should().Be("ac");
	}

	[Fact]
	public void ReplaceNonWordCharacters_ReturnsRemovedPunctuationCharacteres()
	{
		//arrange
		string? text = "a,c";

		//act
		var result = text.ReplaceNonWordCharacters(null);

		//assert
		result.Should().Be("ac");
	}

	[Fact]
	public void HasNonWordCharacters_ReturnsTrueForControl()
	{
		//arrange
		string? text = "a\nc";

		//act
		var result = text.HasNonWordCharacters();

		//assert
		result.Should().BeTrue();
	}

	[Fact]
	public void HasNonWordCharacters_ReturnsTrueForPunctuation()
	{
		//arrange
		string? text = "a,c";

		//act
		var result = text.HasNonWordCharacters();

		//assert
		result.Should().BeTrue();
	}
}
