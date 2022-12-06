namespace Solutions.Day6;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data) =>
        Search(data, 4);

    public static string SolvePart2(IEnumerable<string> data) =>
        Search(data, 14);

    private static string Search(IEnumerable<string> data, int bufferSize)
    {
        var stream = data.First().ToCharArray();
        var buffer = new List<char>();

        for (var i = 0; i < stream.Length; i++)
        {
            if (buffer.Distinct().Count() == bufferSize)
                return i.ToString();

            if (buffer.Count < bufferSize)
            {
                buffer.Add(stream[i]);
                continue;
            }

            buffer.Add(stream[i]);
            buffer.RemoveAt(0);
        }

        return string.Empty;
    }
}