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
        var (map, xOffset) = ParseMap(data);
        var sandCount = 0;
        var dropPoint = (x: 500 - xOffset, y: 0); 
        var sand = dropPoint;
        var empty = '\0';
        var infiniteSpace = new HashSet<(int x, int y)>();
        var floorIndex = map.GetLength(1) + 1;

        while (true)
        {
            if (sand.y + 1 >= map.GetLength(1)
                || sand.x - 1 < 0
                || sand.x + 1 >= map.GetLength(0))
            {
                if (!infiniteSpace.Contains((sand.x, sand.y + 1)) 
                    && sand.y + 1 < floorIndex
                    && (!map.IsInBounds((sand.x, sand.y + 1)) 
                        || map[sand.x, sand.y + 1] == empty))
                {
                    sand.y++;
                }
                else if (!infiniteSpace.Contains((sand.x - 1, sand.y + 1)) 
                         && sand.y + 1 < floorIndex
                         && (!map.IsInBounds((sand.x - 1, sand.y + 1)) 
                             || map[sand.x - 1, sand.y + 1] == empty))
                {
                    sand.x--;
                    sand.y++;
                }
                else if (!infiniteSpace.Contains((sand.x + 1, sand.y + 1)) 
                         && sand.y + 1 < floorIndex
                         && (!map.IsInBounds((sand.x + 1, sand.y + 1))
                            || map[sand.x + 1, sand.y + 1] == empty))
                {
                    sand.x++;
                    sand.y++;
                }
                else
                {
                    if (map.IsInBounds(sand))
                    {
                        map[sand.x, sand.y] = 'o';
                    }
                    
                    sandCount++;
                    infiniteSpace.Add((sand.x, sand.y));
                    sand = dropPoint;
                }
            }
            else if (map[sand.x, sand.y + 1] == empty)
            {
                sand.y++;
            }
            else if (map[sand.x - 1, sand.y + 1] == empty)
            {
                sand.x--;
                sand.y++;
            }
            else if (map[sand.x + 1, sand.y + 1] == empty)
            {
                sand.x++;
                sand.y++;
            }
            else
            {
                if (map[dropPoint.x, dropPoint.y] != empty)
                    break;
                
                sandCount++;
                map[sand.x, sand.y] = 'o';
                sand = dropPoint;
                // PrintMap2(map, infiniteSpace);
            }
        }
        
        return sandCount.ToString();
    }

    private static bool IsInBounds(this char[,] map, (int x, int y) point) =>
        point.x >= 0
        && point.x < map.GetLength(0)
        && point.y >= 0
        && point.y < map.GetLength(1);

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

        Console.WriteLine();
    }
    
    private static void PrintMap2(char[,] map, HashSet<(int x, int y)> infiniteSpace)
    {
        var infMinX = infiniteSpace.Any() ? infiniteSpace.Min(set => set.x) : 0;
        var infMaxX = infiniteSpace.Any() ? infiniteSpace.Max(set => set.x) : 0;
        var infMinY = infiniteSpace.Any() ? infiniteSpace.Min(set => set.y) : 0;
        var infMaxY = infiniteSpace.Any() ? infiniteSpace.Max(set => set.y) : 0;
        
        var minX = Math.Min(0, infMinX);
        var maxX = Math.Max(map.GetLength(0), infMaxX);
        var minY = Math.Min(0, infMinY);
        var maxY = Math.Max(map.GetLength(1), infMaxY);
        
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                if (map.IsInBounds((x, y)) && map[x, y] != '\0')
                    Console.Write(map[x, y]);
                else if (infiniteSpace.Contains((x, y)))
                    Console.Write("o");
                else 
                    Console.Write(".");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}