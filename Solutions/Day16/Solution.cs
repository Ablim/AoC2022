namespace Solutions.Day16;

public static class Solution
{
    public static int Day => 16;
    
    private static Dictionary<string, int> _rates = new();
    private static Dictionary<string, List<string>> _links = new();

    public static string SolvePart1(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        _rates = ParseRates(dataList);
        _links = ParseLinks(dataList);
        
        return LongestPath("AA", 30, 0, new HashSet<string>(), new HashSet<string>()).ToString();
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

    private static int LongestPath(string valve, int minutes, int length, HashSet<string> openValves, HashSet<string> visitedEdges)
    {
        if (minutes == 0)
            return length;

        minutes--;
        length += _rates
            .Where(r => openValves.Contains(r.Key))
            .Sum(r => r.Value);
        
        var lengths = new List<int>();

        // test to open valve
        if (!openValves.Contains(valve) && _rates[valve] > 0)
        {
            var openValvesCopy = new HashSet<string>(openValves)
            {
                valve
            };
            lengths.Add(LongestPath(valve, minutes, length, openValvesCopy, visitedEdges));
        }

        // test adjacent paths
        foreach (var other in _links[valve])
        {
            var edgeKey = $"{valve}{other}";
            if (visitedEdges.Contains(edgeKey))
                continue;

            var visitedEdgesCopy = new HashSet<string>(visitedEdges)
            {
                edgeKey
            };
            lengths.Add(LongestPath(other, minutes, length, openValves, visitedEdgesCopy));
        }
        
        // if no other options remain, stay put
        if (!lengths.Any())
            lengths.Add(LongestPath(valve, minutes, length, openValves, visitedEdges));
        
        return lengths.Max();
    }
}