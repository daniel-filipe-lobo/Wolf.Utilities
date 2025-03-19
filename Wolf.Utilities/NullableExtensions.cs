namespace Wolf.Utilities
{
	public static class NullableExtensions
	{
		public static string ThrowIfIsNullOrWhiteSpace([NotNull] this string? value, string? name = null)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new Exception($"Value {name} is null or white space");
			}
			return value;
		}

		public static DateTimeOffset ThrowIfIsNull([NotNull] this DateTimeOffset? value, string? name = null)
		{
			if (value == null)
			{
				throw new Exception($"Value {name} is null");
			}
			return value.Value;
		}

		[return: NotNull]
		public static T ThrowIfIsNull<T>([NotNull] this T? value, string? name = null)
			where T : struct
		{
			if (value == null)
			{
				throw new Exception($"Value {name} is null");
			}
			return value.Value;
		}

		[return: NotNull]
		public static T ThrowIfIsNull<T>([NotNull] this T? value, string? name = null)
			where T : class
		{
			if (value == null)
			{
				throw new Exception($"Value {name} is null");
			}
			return value;
		}

		public static IEnumerable<T> ThrowIfIsNullOrElementIsNull<T>([NotNull] this IEnumerable<T?>? values, string? name = null)
			where T : class
		{
			if (values == null || values.Any(value => value == null))
			{
				throw new Exception($"Value {name} is null");
			}
			return (IEnumerable<T>)values;
		}

		public static IEnumerable<T> ToEmptyIfIsNull<T>(this IEnumerable<T>? values)
		{
			return values ?? Enumerable.Empty<T>();
		}

		public static IEnumerable<T>? ToNullIfIsEmpty<T>(this IEnumerable<T>? values)
		{
			return values?.Any() == true ? values : null;
		}
	}
}
