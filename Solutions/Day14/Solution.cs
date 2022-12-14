namespace Solutions.Day14;

public static class Solution
{
    public static int Day => 14;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var (map, xOffset) = ParseMap(data);
        var sandCount = 0;
        var dropPoint = (x: 500 - xOffset, y: 0); 
        var currentSand = dropPoint;
        var empty = '\0';

        while (true)
        {
            if (currentSand.y + 1 == map.GetLength(1)
                || currentSand.x - 1 == -1
                || currentSand.x + 1 == map.GetLength(0))
                break;

            if (map[currentSand.x, currentSand.y + 1] == empty)
            {
                currentSand.y++;
            }
            else if (map[currentSand.x - 1, currentSand.y + 1] == empty)
            {
                currentSand.x--;
                currentSand.y++;
            }
            else if (map[currentSand.x + 1, currentSand.y + 1] == empty)
            {
                currentSand.x++;
                currentSand.y++;
            }
            else
            {
                sandCount++;
                map[currentSand.x, currentSand.y] = 'o';
                currentSand = dropPoint;
                // PrintMap(map);
            }
        }
        
        return sandCount.ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }

    private static (char[,], int) ParseMap(IEnumerable<string> data)
    {
        var lines = data.Select(row =>
            {
                var points = row.Split(" -> ");
                return points.Select(p =>
                    {
                        var parts = p.Split(",");
                        return (x: int.Parse(parts[0]), y: int.Parse(parts[1]));
                    })
                    .ToList();
            })
            .ToList();
        
        var allPoints = lines.SelectMany(line => line)
            .ToList();
        var xOffset = allPoints.Min(p => p.x);
        var xMax = allPoints.Max(p => p.x);
        var yMax = allPoints.Max(p => p.y); 
        var width = xMax - xOffset + 1;                             
        var height = yMax + 1;
        
        var map = new char[width, height];
        lines.ForEach(l =>
        {
            for (var i = 0; i < l.Count - 1; i++)
            {
                var a = l[i];
                var b = l[i + 1];

                if (a.x == b.x)
                {
                    for (var y = Math.Min(a.y, b.y); y <= Math.Max(a.y, b.y); y++)
                    {
                        map[a.x - xOffset, y] = '#';
                    }
                }
                
                if (a.y == b.y)
                {
                    for (var x = Math.Min(a.x, b.x); x <= Math.Max(a.x, b.x); x++)
                    {
                        map[x - xOffset, a.y] = '#';
                    }
                }
            }
        });

        return (map, xOffset);
    }
    
    private static void PrintMap(char[,] map)
    {
        for (var y = 0; y < map.GetLength(1); y++)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                Console.Write(map[x, y] != '\0' ? map[x, y] : ".");
            }

            Console.WriteLine();
        }
    }
}