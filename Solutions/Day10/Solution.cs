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
        var dataList = data.ToList();
        var display = new char[240];
        var register = 1;
        var instruction = string.Empty;

        for (var i = 0; i < 240; i++)
        {
            if (i > 0 && i % 40 == 0)
                register += 40;
            
            if (i >= register - 1 && i <= register + 1)
                display[i] = '#';
            else
                display[i] = '.';

            if (instruction == string.Empty)
            {
                var temp = dataList.First();
                dataList.RemoveAt(0);

                if (temp.StartsWith("addx"))
                    instruction = temp;
            }
            else
            {
                register += int.Parse(instruction.Split(' ')[1]);
                instruction = string.Empty;
            }
        }
        
        display.Print();
        return register.ToString();
    }

    private static void Print(this char[] display)
    {
        for (var i = 0; i < display.Length; i++)
        {
            if (i > 0 && i % 40 == 0)
                Console.WriteLine();
            
            Console.Write(display[i]);
        }

        Console.WriteLine();
        Console.WriteLine();
    }
}