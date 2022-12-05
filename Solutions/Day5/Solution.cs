namespace Solutions.Day5;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        var stacks = ParseStacks(dataList);
        var moves = ParseMoves(dataList);
        Apply(stacks, moves);

        return new string(stacks.SelectMany(s => s.Value.TakeLast(1)).ToArray());
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        var stacks = ParseStacks(dataList);
        var moves = ParseMoves(dataList);
        ApplyNoReverse(stacks, moves);

        return new string(stacks.SelectMany(s => s.Value.TakeLast(1)).ToArray());
    }

    private static Dictionary<int, List<char>> ParseStacks(IEnumerable<string> data)
    {
        var stackRows = data
            .TakeWhile(row => !string.IsNullOrEmpty(row))
            .Reverse()
            .ToList();
        
        var indices = stackRows
            .First()
            .Split("   ")
            .Select(x => x.Trim())
            .Select(int.Parse)
            .ToList();
        
        var stacks = indices.ToDictionary(x => x, _ => new List<char>());
        var boxRows = stackRows.Take(new Range(1, stackRows.Count));

        foreach (var row in boxRows)
        {
            var charIndex = 1;
            var stack = 1;
            while (charIndex < row.Length)
            {
                var box = row[charIndex];
                if (box != ' ')
                    stacks[stack].Add(box);
                
                charIndex += 4;
                stack++;
            }
        }

        return stacks;
    }

    private static IEnumerable<(int take, int from, int to)> ParseMoves(IEnumerable<string> data)
    {
        return data
            .Reverse()
            .TakeWhile(row => !string.IsNullOrEmpty(row))
            .Reverse()
            .Select(row => row.Split(' '))
            .Select(row => (int.Parse(row[1]), int.Parse(row[3]), int.Parse(row[5])));
    }
    
    private static void Apply(Dictionary<int, List<char>> stacks, IEnumerable<(int take, int from, int to)> moves)
    {
        foreach (var (take, from, to) in moves)
        {
            var boxes = stacks[from]
                .TakeLast(take)
                .Reverse()
                .ToList();
            stacks[from].RemoveRange(stacks[from].Count - take, take);
            stacks[to].AddRange(boxes);
        }
    }
    
    private static void ApplyNoReverse(Dictionary<int, List<char>> stacks, IEnumerable<(int take, int from, int to)> moves)
    {
        foreach (var (take, from, to) in moves)
        {
            var boxes = stacks[from]
                .TakeLast(take)
                .ToList();
            stacks[from].RemoveRange(stacks[from].Count - take, take);
            stacks[to].AddRange(boxes);
        }
    }
}