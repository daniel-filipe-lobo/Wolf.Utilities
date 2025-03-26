namespace Wolf.Utilities;

public static class IEnumerableExtensions
{
	[return: NotNull]
	public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
	{
		return source ?? [];
	}

	public static IEnumerable<T>? NullIfIsEmpty<T>(this IEnumerable<T>? values)
	{
		return values?.Any() == true ? values : null;
	}
}
