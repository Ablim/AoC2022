namespace Solutions.Day3;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data)
    {
        return data.Select(GetPriority)
            .Sum()
            .ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var rows = data.ToList();
        var groups = new List<List<string>>();
        
        while (rows.Any())
        {
            groups.Add(rows.Take(3).ToList());
            rows.RemoveRange(0, 3);
        }

        return groups.Select(g => g[0].Intersect(g[1].Intersect(g[2])))
            .Select(x => x.FirstOrDefault().ToPriority())
            .Sum()
            .ToString();
    }

    private static int GetPriority(string rucksack) 
    {
        var compSize = rucksack.Length / 2;
        var leftComp = rucksack[..compSize].ToCharArray();
        var rightComp = rucksack[compSize..].ToCharArray();

        return leftComp.Intersect(rightComp)
            .Select(ToPriority)
            .Sum();
    }

    private static int ToPriority(this char item) =>
        (item >= 'a' && item <= 'z')
            ? item - 96 : item - 38;
}