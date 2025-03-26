using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wolf.Utilities;

public static partial class StringExtensions
{
	[GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
	private static partial Regex EmailRegex();

	public static string EscapeBraces(this string text)
	{
		return text.Replace("{", "{{").Replace("}", "}}");
	}

	/// <summary>
	/// Replace XPath special characters for their escaped version
	/// </summary>
	/// <param name="xPath"></param>
	/// <returns></returns>
	public static string XpathEscape(this string xPath)
	{
		return xPath
			.Replace("'", "\\'")
			.Replace("\"", "\\\"")
			.Replace("\\", "\\\\");
	}

	//https://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net
	[return: NotNullIfNotNull(nameof(text))]
	private static (string? CleanText, bool HasDiacritics) Diacritics(this string? text, bool isRemove)
	{
		bool hasDiacritics = false;
		if (string.IsNullOrWhiteSpace(text))
		{
			return (text, hasDiacritics);
		}
		var normalizedString = text.Normalize(NormalizationForm.FormD);
		var length = normalizedString.Length;
		var stringBuilder = new StringBuilder(capacity: length);

		for (int index = 0; index < length; ++index)
		{
			char character = normalizedString[index];
			var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
			if (unicodeCategory == UnicodeCategory.NonSpacingMark)
			{
				hasDiacritics = true;
				if (isRemove)
				{
					continue;
				}
				else
				{
					return (text, hasDiacritics);
				}
			}
			stringBuilder.Append(character);
		}
		return (stringBuilder.ToString().Normalize(NormalizationForm.FormC), hasDiacritics);
	}

	public static string? RemoveDiacritics(this string? text)
	{
		return Diacritics(text, isRemove: true).CleanText;
	}

	public static bool HasDiacritics(this string? text)
	{
		return Diacritics(text, isRemove: false).HasDiacritics;
	}

	public static bool IsValidEmail([NotNullWhen(true)] this string? email)
	{
		return email != null && EmailRegex().Match(email).Success;
	}

	public static string HashSha512ToBase64(this string text)
	{
		return Convert.ToBase64String(SHA512.HashData(Encoding.UTF8.GetBytes(text)));
	}

	public static string GetValidFileName(this string fileName)
	{
		string invalid = new(Path.GetInvalidFileNameChars());
		foreach (char character in invalid)
		{
			fileName = fileName.Replace(character.ToString(), "");
		}
		return fileName.Trim();
	}

	public static string CamelCase(this string text)
	{
		if (string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		var textEnd = text.Length > 1 ? text[1..] : "";
		return $"{char.ToLower(text[0])}{textEnd}";
	}

	private static (string CleanText, bool HasExtraSpaces) ExtraSpaces(this string text, bool isReplace)
	{
		bool hasExtraSpaces = false;
		List<char> newText = [];
		int separatorCount = 0;
		foreach (var character in text)
		{
			char newCharacter = character;
			if (char.IsSeparator(newCharacter)) // removes extra separators
			{
				if (separatorCount++ >= 1)
				{
					hasExtraSpaces = true;
					if (!isReplace)
					{
						return (text, hasExtraSpaces);
					}
					continue;
				}
			}
			else
			{
				separatorCount = 0;
			}
			newText.Add(newCharacter);
		}
		return (new string([.. newText]), hasExtraSpaces);
	}

	public static string RemoveExtraSpaces(this string text)
	{
		return ExtraSpaces(text, isReplace: true).CleanText;
	}

	public static bool HasExtraSpaces(this string text)
	{
		return ExtraSpaces(text, isReplace: false).HasExtraSpaces;
	}

	private static (string CleanText, bool HasNonWordCaracters) NonWordCharacters(string text, char? replaceCharacter, bool isReplace)
	{
		bool hasNonWordCaracters = false;
		List<char> newText = [];
		foreach (var character in text)
		{
			char newCharacter = character;
			if (char.IsPunctuation(newCharacter) && newCharacter != '-')
			{
				hasNonWordCaracters = true;
				if (!isReplace)
				{
					return (text, hasNonWordCaracters);
				}
				if (replaceCharacter == null)
				{
					continue;
				}
				newCharacter = replaceCharacter.Value;
			}
			if (char.IsControl(newCharacter) || char.IsSymbol(newCharacter))
			{
				hasNonWordCaracters = true;
				if (!isReplace)
				{
					return (text, hasNonWordCaracters);
				}
				continue;
			}
			newText.Add(newCharacter);
		}
		return (new string([.. newText]), hasNonWordCaracters);
	}

	public static string ReplaceNonWordCharacters(this string text, char? replaceCharacter)
	{
		return NonWordCharacters(text, replaceCharacter, isReplace: true).CleanText;
	}

	public static bool HasNonWordCharacters(this string text)
	{
		return NonWordCharacters(text, null, isReplace: false).HasNonWordCaracters;
	}
}
