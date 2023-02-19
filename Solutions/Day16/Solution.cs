namespace Solutions.Day16;

public static class Solution
{
    public static int Day => 16;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        var rates = ParseRates(dataList);
        var links = ParseLinks(dataList);
        
        return LongestPath(rates, links, "AA", 30, 0, new HashSet<string>()).ToString();
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

    private static int LongestPath(Dictionary<string, int> rates, Dictionary<string, List<string>> links, string valve,
        int minutes, int length, HashSet<string> openValves)
    {
        if (minutes == 0)
            return length;

        minutes--;
        length += rates
            .Where(r => openValves.Contains(r.Key))
            .Sum(r => r.Value);
        
        // if valve is not open, choose open or where to go next
        // if valve is open, choose where to go next
        var lengths = new List<int>();

        if (!openValves.Contains(valve) && rates[valve] > 0)
        {
            var openValvesCopy = new HashSet<string>(openValves)
            {
                valve
            };
            lengths.Add(LongestPath(rates, links, valve, minutes, length, openValvesCopy));
        }

        foreach (var other in links[valve])
        {
            lengths.Add(LongestPath(rates, links, other, minutes, length, openValves));
        }
        
        return lengths.Any() ? lengths.Max() : 0;
    }
}
