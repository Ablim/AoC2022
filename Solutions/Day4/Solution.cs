namespace Solutions.Day4;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data) =>
        data.Parse()
            .Select(pair => AIsInB(pair.Item1, pair.Item2, pair.Item3, pair.Item4)
                            || AIsInB(pair.Item3, pair.Item4, pair.Item1, pair.Item2) ? 1 : 0)
            .Sum()
            .ToString();

    public static string SolvePart2(IEnumerable<string> data) =>
        data.Parse()
            .Select(pair => AOverlapsB(pair.Item1, pair.Item2, pair.Item3, pair.Item4) ? 1 : 0)
            .Sum()
            .ToString();

    private static IEnumerable<(int, int, int, int)> Parse(this IEnumerable<string> data) =>
        data.Select(row => row.Split(','))
            .Select(pair => (pair[0].Split('-'), pair[1].Split('-')))
            .Select(pair => (int.Parse(pair.Item1[0]), int.Parse(pair.Item1[1]), int.Parse(pair.Item2[0]),
                int.Parse(pair.Item2[1])));

    private static bool AOverlapsB(int aLeft, int aRight, int bLeft, int bRight) =>
        aLeft >= bLeft && aLeft <= bRight
        || aRight >= bLeft && aRight <= bRight
        || bLeft >= aLeft && bLeft <= aRight
        || bRight >= aLeft && bRight <= aRight;

    private static bool AIsInB(int aLeft, int aRight, int bLeft, int bRight) =>
        aLeft >= bLeft && aRight <= bRight;
}