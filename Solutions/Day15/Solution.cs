namespace Solutions.Day15;

public static class Solution
{
    public static int Day => 15;
    
    public static string SolvePart1(IEnumerable<string> data, int rowIndex)
    {
        var pairs = Parse(data);
        var leftMostSensor = pairs.MinBy(p => p.sX - p.distance);
        var rightMostSensor = pairs.MaxBy(p => p.sX + p.distance);
        var minX = leftMostSensor.sX - leftMostSensor.distance;
        var maxX = rightMostSensor.sX + rightMostSensor.distance;
        var counter = 0;
        
        for (var x = minX; x <= maxX; x++)
        {
            foreach (var pair in pairs)
            {
                if (x == pair.sX && rowIndex == pair.sY || x == pair.bX && rowIndex == pair.bY)
                    continue;

                var distance = GetDistance((x, rowIndex), (pair.sX, pair.sY));
                if (distance <= pair.distance)
                {
                    counter++;
                    break;
                }
            }
        }
        
        return counter.ToString();
    }
    
    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }
    
    private static List<(int sX, int sY, int bX, int bY, int distance)> Parse(IEnumerable<string> data) => data
        .Select(row => row
            .Replace("Sensor at ", "")
            .Replace("closest beacon is at ", "")
            .Replace("x=", "")
            .Replace(", y=", " ")
            .Replace(":", "")
            .Split(" "))
        .Select(row => row.Select(int.Parse).ToArray())
        .Select(row => (sX: row[0], sY: row[1], bX: row[2], bY: row[3], distance: GetDistance((row[0], row[1]), (row[2], row[3]))))
        .ToList();

    private static int GetDistance((int x, int y) a, (int x, int y) b) =>
        Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
}