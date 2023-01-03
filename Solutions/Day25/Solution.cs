namespace Solutions.Day25;

public static class Solution
{
    public static int Day => 25;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        return data
            .Select(ToDecimal)
            .LongSum()
            .ToSnafu();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }

    private static long ToDecimal(this string number) => number
        .ToCharArray()
        .Select(MapSnafu)
        .ToArray()
        .Evaluate();

    private static long Evaluate(this int[] snafuParts)
    {
        var decimalParts = new long[snafuParts.Length];

        for (var i = 0; i < snafuParts.Length; i++)
        {
            var power = snafuParts.Length - 1 - i;
            decimalParts[i] = snafuParts[i] * (long)Math.Pow(5, power);
        }
        
        return decimalParts.LongSum();
    }
    
    private static string ToSnafu(this long number)
    {
        var exponent = 0;
        while (2 * Math.Pow(5, exponent) < number)
        {
            exponent++;
        }

        return new string(Explore(new int[exponent + 1], 0, number)
            .Select(MapSnafu)
            .ToArray());
    }

    private static int[] Explore(int[] word, int index, long target)
    {
        if (index == word.Length)
            return word;
        
        for (var i = -2; i <= 2; i++)
        {
            var copy = new int[word.Length];
            Array.Copy(word, copy, word.Length);
            copy[index] = i;
            
            var result = Explore(copy, index + 1, target);
            if (result.Evaluate() == target)
                return result;
        }

        return Array.Empty<int>();
    }

    private static int MapSnafu(this char snafu) =>
        snafu switch
        {
            '2' => 2,
            '1' => 1,
            '0' => 0,
            '-' => -1,
            '=' => -2,
            _ => throw new InvalidOperationException()
        };
    
    private static char MapSnafu(this int dec) =>
        dec switch
        {
            2 => '2',
            1 => '1',
            0 => '0',
            -1 => '-',
            -2 => '=',
            _ => throw new InvalidOperationException()
        };

    private static long LongSum(this IEnumerable<long> word) =>
        word.Aggregate<long, long>(0, (current, number) => current + number);
}