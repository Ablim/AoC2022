namespace Solutions.Day1;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data)
    {
        return SumSort(data)
            .FirstOrDefault()
            .ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return SumSort(data)
            .Take(3)
            .Sum()
            .ToString();
    }

    private static IEnumerable<int> SumSort(IEnumerable<string> data)
    {
        var elves = new List<List<int>>();
        var calories = new List<int>();
        
        foreach (var row in data)
        {
            if (int.TryParse(row, out var result))
            {
                calories.Add(result);
            }
            else
            {
                elves.Add(calories);
                calories = new List<int>();
            }
        }
        
        elves.Add(calories);

        return elves.Select(e => e.Sum())
            .OrderByDescending(sum => sum);
    }
}