namespace Solutions.Day25;

public static class Solution
{
    public static int Day => 25;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        return data
            .Select(ToDecimal)
            .Sum()
            .ToSnafu();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }

    private static int ToDecimal(this string number)
    {
        var snafuParts = number
            .ToCharArray()
            .Select(MapSnafu)
            .ToArray();
        var decimalParts = new int[snafuParts.Length];

        for (var i = 0; i < snafuParts.Length; i++)
        {
            var power = snafuParts.Length - 1 - i;
            decimalParts[i] = snafuParts[i] * (int)Math.Pow(5, power);
        }
        
        return decimalParts.Sum();
    }
    
    private static string ToSnafu(this int number)
    {
        var decimalParts = number
            .ToString()
            .ToCharArray()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();
        var snafuParts = new List<int>();

        for (var i = 0; i < decimalParts.Length; i++)
        {
            var power = decimalParts.Length - 1 - i;
            var decimalValue = decimalParts[i] * Math.Pow(10, power);
            
            // x * 5^y = 4000
            var snafuPower = 0;
            var decimalCopy = decimalValue;
                
            while (decimalCopy / 5 >= 1)
            {
                decimalCopy /= 5;
                snafuPower++;
            }

            var snafuFactor = decimalValue / Math.Pow(5, snafuPower);
            snafuParts.Add((int)snafuFactor);
        }
        
        return new string(snafuParts
            .Select(MapSnafu)
            .ToArray());
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
}