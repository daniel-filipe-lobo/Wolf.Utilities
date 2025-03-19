namespace Wolf.Utilities
{
	public static partial class StringExtensions
	{
		public static string EscapeBraces(this string text)
		{
			return text.Replace("{", "{{").Replace("}", "}}");
		}

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

		public static int CalculateEditDistance(this string currentString, string secondString, int maxAllowedDistance)
		{
			string firstString = currentString;

			int firstStringLength = firstString.Length;
			int secondStringLength = secondString.Length;

			if (firstStringLength == 0)
				return secondStringLength;

			if (secondStringLength == 0) return firstStringLength;

			if (firstStringLength > secondStringLength)
			{
				(secondString, firstString) = (firstString, secondString);
				firstStringLength = secondStringLength;
				secondStringLength = secondString.Length;
			}

			int[] currentRow = new int[firstStringLength + 1];
			int[] previousRow = new int[firstStringLength + 1];
			int[] transpositionRow = new int[firstStringLength + 1];

			if (maxAllowedDistance < 0) maxAllowedDistance = secondStringLength;
			if (secondStringLength - firstStringLength > maxAllowedDistance) return maxAllowedDistance + 1;

			for (int i = 0; i <= firstStringLength; i++)
				previousRow[i] = i;

			char secondStringLastCheckedChar = default;
			for (int i = 1; i <= secondStringLength; i++)
			{
				char secondStringCharToCheck = secondString[i - 1];
				currentRow[0] = i;

				// Compute only diagonal stripe of width 2 * (max + 1)
				int from = Math.Max(i - maxAllowedDistance - 1, 1);
				int to = Math.Min(i + maxAllowedDistance + 1, firstStringLength);

				char firstStringLastCheckedChar = default;
				for (int j = from; j <= to; j++)
				{
					char firstStringCharToCheck = firstString[j - 1];

					// Compute minimal cost of state change to current state from previous states of deletion, insertion and swapping 
					int cost = string.Compare(firstStringCharToCheck.ToString(), secondStringCharToCheck.ToString(), CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0 ? 0 : 1;
					int value = Math.Min(Math.Min(currentRow[j - 1] + 1, previousRow[j] + 1), previousRow[j - 1] + cost);

					// If there was transposition, take in account its cost 
					if (string.Compare(firstStringCharToCheck.ToString(), secondStringLastCheckedChar.ToString(), CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0
						&& string.Compare(firstStringLastCheckedChar.ToString(), secondStringCharToCheck.ToString(), CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
					{
						value = Math.Min(value, transpositionRow[j - 2] + cost);
					}

					currentRow[j] = value;
					firstStringLastCheckedChar = firstStringCharToCheck;
				}
				secondStringLastCheckedChar = secondStringCharToCheck;

				(currentRow, previousRow, transpositionRow) = (transpositionRow, currentRow, previousRow);
			}

			return previousRow[firstStringLength];
		}

		public static bool IsValidEmail([NotNullWhen(true)] this string? email)
		{
			return email != null && EmailRegex().Match(email).Success;
		}

		public static int CountExpressions(this string text)
		{
			return GetExpressions(text).Count();
		}

		public static IEnumerable<string> GetExpressions(this string text)
		{
			return ExpressionsRegex().Matches(text).SelectMany(match => match.Captures.Select(expression => expression.Value)) ?? [];
		}

		[GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
		private static partial Regex EmailRegex();

		[GeneratedRegex(@""".+?""|\w+")]
		private static partial Regex ExpressionsRegex();

		public static string HashDataSha512(this string text)
		{
			return Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(text)));
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
}
