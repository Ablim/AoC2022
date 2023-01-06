namespace Solutions.Day10;

public static class Solution
{
    public static int Day => 10;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var regX = 1;
        var cycle = 1;
        var sum = 0;
        var checkpoints = new[] { 20, 60, 100, 140, 180, 220 };
        
        foreach (var instruction in data)
        {
            if (instruction.StartsWith("addx"))
            {
                // Cycle 1
                if (checkpoints.Contains(cycle))
                    sum += cycle * regX;

                cycle++;
                
                // Cycle 2
                if (checkpoints.Contains(cycle))
                    sum += cycle * regX;
                
                var value = int.Parse(instruction.Split(' ')[1]);
                regX += value;
                cycle++;
            }

            if (instruction.StartsWith("noop"))
            {
                if (checkpoints.Contains(cycle))
                    sum += cycle * regX;
                
                cycle++;
            }
        }
        
        return sum.ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }
}