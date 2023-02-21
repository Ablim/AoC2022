namespace Solutions.Day16;

public static class Solution
{
    public static int Day => 16;
    
    private static Dictionary<string, int> _rates = new();
    private static Dictionary<string, List<string>> _links = new();
    private static int _completedPaths = 0;

    public static string SolvePart1(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        _rates = ParseRates(dataList);
        _links = ParseLinks(dataList);
        
        var length = LongestPath("AA", 30, 0, 0, new HashSet<string>(), new Dictionary<string, int>{{"AA", 1}}).ToString();
        Console.WriteLine($"Paths: {_completedPaths}");
        return length;
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return string.Empty;
    }

    private static int ParseInt(this string word) => int.Parse(word);

    private static Dictionary<string, int> ParseRates(List<string> data)
    {
        var rates = new Dictionary<string, int>();
        
        foreach (var row in data)
        {
            var parts = row.Split(' ');
            var valve = parts[1];
            var rate = parts[4]
                .Replace("rate=", "")
                .Replace(";", "")
                .ParseInt();
            
            rates.Add(valve, rate);
        }

        return rates;
    }
    
    private static Dictionary<string, List<string>> ParseLinks(List<string> data)
    {
        var links = new Dictionary<string, List<string>>();
        
        foreach (var row in data)
        {
            var parts = row.Split(' ');
            var valve = parts[1];
            var otherValves = parts.Skip(9)
                .Select(p => p.Replace(",", ""))
                .ToList();
            links.Add(valve, otherValves);
        }

        return links;
    }

    private static int LongestPath(string valve, int minutes, int length, int changeRate, HashSet<string> openValves, Dictionary<string, int> visitCount)
    {
        if (minutes == 0)
        {
            _completedPaths++;
            return length;
        }

        minutes--;
        length += changeRate;
        var lengths = new List<int>();

        // test to open valve
        if (!openValves.Contains(valve) && _rates[valve] > 0)
        {
            var openValvesCopy = new HashSet<string>(openValves)
            {
                valve
            };
            lengths.Add(LongestPath(valve, minutes, length, changeRate + _rates[valve], openValvesCopy, visitCount));
        }

        // test adjacent paths
        foreach (var other in _links[valve])
        {
            if (visitCount.ContainsKey(other) && visitCount[other] == _links[other].Count)
                continue;

            var visitCountCopy = new Dictionary<string, int>(visitCount);
            if (visitCountCopy.ContainsKey(other))
                visitCountCopy[other]++;
            else
                visitCountCopy.Add(other, 1);
            
            lengths.Add(LongestPath(other, minutes, length, changeRate, openValves, visitCountCopy));
        }
        
        // if no other options remain, stay put
        if (!lengths.Any())
        {
            for (var i = minutes; i > 0; i--)
            {
                length += changeRate;
            }
            
            lengths.Add(LongestPath(valve, 0, length, changeRate, openValves, visitCount));
        }
        
        return lengths.Max();
    }
}
