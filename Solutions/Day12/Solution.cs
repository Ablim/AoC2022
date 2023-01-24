namespace Solutions.Day12;

public static class Solution
{
    public static int Day => 12;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var map = data
            .Select(row => row.ToArray())
            .ToArray();
        var mapCost = BuildCostMap(map.Length, map[0].Length);
        var start = Find(map, 'S');
        var paths = new SortedSet<Path>();
        paths.Add(new Path
        {
            Head = start,
            Steps = new HashSet<Point>
            {
                start
            }
        });

        while (paths.Any())
        {
            var path = paths.Min ?? throw new InvalidOperationException();
            var current = map[path.Head.Row][path.Head.Column];

            if (current == 'E')
                break;

            if (path.Head.Row - 1 >= 0)
            {
                var upPoint = new Point
                {
                    Row = path.Head.Row - 1,
                    Column = path.Head.Column
                };
                CheckStep(current, upPoint, map, path, paths, mapCost);
            }
            
            if (path.Head.Row + 1 < map.Length)
            {
                var downPoint = new Point
                {
                    Row = path.Head.Row + 1,
                    Column = path.Head.Column
                };
                CheckStep(current, downPoint, map, path, paths, mapCost);
            }
            
            if (path.Head.Column - 1 >= 0)
            {
                var leftPoint = new Point
                {
                    Row = path.Head.Row,
                    Column = path.Head.Column - 1
                };
                CheckStep(current, leftPoint, map, path, paths, mapCost);
            }
            
            if (path.Head.Column + 1 < map[0].Length)
            {
                var rightPoint = new Point
                {
                    Row = path.Head.Row,
                    Column = path.Head.Column + 1
                };
                CheckStep(current, rightPoint, map, path, paths, mapCost);
            }

            paths.Remove(path);
        }
        
        // Print(mapCost);
        return (paths.Min?.Length - 1).ToString() ?? "0";
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }

    private static Point Find(char[][] map, char point)
    {
        for (var row = 0; row < map.Length; row++)
        {
            for (var col = 0; col < map[0].Length; col++)
            {
                if (map[row][col] == point)
                    return new Point
                    {
                        Row = row,
                        Column = col
                    };
            }
        }

        return new Point();
    }

    private static int Score(this char level) =>
        level switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => level
        };

    private static void CheckStep(char level, Point point, char[][] map, Path shortest, SortedSet<Path> paths, int[,] mapCost)
    {
        var next = map[point.Row][point.Column];
        
        if (next.Score() <= level.Score() + 1
            && !shortest.Steps.Contains(point)
            && shortest.Length + 1 < mapCost[point.Row, point.Column])
        {
            paths.Add(new Path
            {
                Head = point,
                Steps = new HashSet<Point>(shortest.Steps)
                {
                    point
                }
            });
            mapCost[point.Row, point.Column] = shortest.Length + 1;
        }
    }

    private static int[,] BuildCostMap(int rows, int columns)
    {
        var costMap = new int[rows, columns];

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < columns; col++)
            {
                costMap[row, col] = int.MaxValue;
            }
        }
        
        return costMap;
    }

    private static void Print(this int[,] costMap)
    {
        for (var row = 0; row < costMap.GetLength(0); row++)
        {
            for (var col = 0; col < costMap.GetLength(1); col++)
            {
                if (costMap[row, col] == int.MaxValue)
                {
                    Console.Write("___ ");
                }
                else
                {
                    var value = costMap[row, col].ToString();
                    if (value.Length == 1)
                    {
                        Console.Write($"  {value} ");
                    }
                    if (value.Length == 2)
                    {
                        Console.Write($" {value} ");
                    }
                    if (value.Length > 2)
                    {
                        Console.Write($"{value} ");
                    }
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}