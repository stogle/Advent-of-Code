namespace AdventOfCode2022.Day08;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> TakeWhileInclusive<T>(this IEnumerable<T> list, Func<T, bool> predicate)
    {
        foreach (T el in list)
        {
            yield return el;
            if (!predicate(el))
                yield break;
        }
    }
}
