namespace Solutions.Day21;

public static class Solution
{
    public static int Day => 21;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var monkeys = data
            .Select(row => row.Split(": "))
            .ToDictionary(row => row[0], row => row[1]);
        return Yell("root", monkeys).ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var monkeys = data
            .Select(row => row.Split(": "))
            .ToDictionary(row => row[0], row => row[1]);
        return Yell2("root", monkeys, 0).ToString();
    }

    private static long Yell(string monkey, Dictionary<string, string> lookup)
    {
        var operation = lookup[monkey];

        if (int.TryParse(operation, out var number))
        {
            return number;
        }

        if (operation.Contains('+'))
        {
            var parts = operation.Split(" + ");
            return Yell(parts[0], lookup) + Yell(parts[1], lookup);
        }
        
        if (operation.Contains('-'))
        {
            var parts = operation.Split(" - ");
            return Yell(parts[0], lookup) - Yell(parts[1], lookup);
        }
        
        if (operation.Contains('*'))
        {
            var parts = operation.Split(" * ");
            return Yell(parts[0], lookup) * Yell(parts[1], lookup);
        }
        
        if (operation.Contains('/'))
        {
            var parts = operation.Split(" / ");
            return Yell(parts[0], lookup) / Yell(parts[1], lookup);
        }

        throw new InvalidOperationException();
    }
    
    private static long Yell2(string monkey, Dictionary<string, string> lookup, long target)
    {
        if (monkey == "humn")
        {
            return target;
        }
        
        var operation = lookup[monkey];
        var parts = operation.Split(" ");
        var (humanSide, otherSide) = ContainsHuman(parts[0], lookup)
            ? (parts[0], parts[2])
            : (parts[2], parts[0]);
        var otherValue = Yell(otherSide, lookup);

        if (monkey == "root")
        {
            return Yell2(humanSide, lookup, otherValue);
        }
        
        if (operation.Contains('+'))
        {
            return Yell2(humanSide, lookup, target - otherValue);
        }
        
        if (operation.Contains('-'))
        {
            var newTarget = parts[0] == otherSide
                ? - (target - otherValue)
                : target + otherValue;
            return Yell2(humanSide, lookup, newTarget);
        }
        
        if (operation.Contains('*'))
        {
            return Yell2(humanSide, lookup, target / otherValue);
        }
        
        if (operation.Contains('/'))
        {
            var newTarget = parts[0] == otherSide
                ? otherValue / target
                : target * otherValue;
            return Yell2(humanSide, lookup, newTarget);
        }

        throw new InvalidOperationException();
    }

    private static bool ContainsHuman(string monkey, Dictionary<string, string> lookup)
    {
        if (monkey == "humn")
            return true;

        var operation = lookup[monkey];

        if (int.TryParse(operation, out _))
            return false;

        var parts = operation.Split(" ");
        return ContainsHuman(parts[0], lookup) || ContainsHuman(parts[2], lookup);
    }
}