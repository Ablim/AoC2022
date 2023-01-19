namespace Solutions.Day12;

public static class Solution
{
    public static int Day => 12;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var map = data
            .Select(row => row.ToArray())
            .ToArray();
        var start = Find(map, 'S');
        var end = Find(map, 'E');
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
                CheckStep(current, upPoint, map, path, paths, end);
            }
            
            if (path.Head.Row + 1 < map.Length)
            {
                var downPoint = new Point
                {
                    Row = path.Head.Row + 1,
                    Column = path.Head.Column
                };
                CheckStep(current, downPoint, map, path, paths, end);
            }
            
            if (path.Head.Column - 1 >= 0)
            {
                var leftPoint = new Point
                {
                    Row = path.Head.Row,
                    Column = path.Head.Column - 1
                };
                CheckStep(current, leftPoint, map, path, paths, end);
            }
            
            if (path.Head.Column + 1 < map[0].Length)
            {
                var rightPoint = new Point
                {
                    Row = path.Head.Row,
                    Column = path.Head.Column + 1
                };
                CheckStep(current, rightPoint, map, path, paths, end);
            }

            paths.Remove(path);
        }
        
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

    private static void CheckStep(char level, Point point, char[][] map, Path shortest, SortedSet<Path> paths, Point end)
    {
        var next = map[point.Row][point.Column];
                
        if (next.Score() >= level.Score() - 1 && next.Score() <= level.Score() + 1)
        {
            if (!shortest.Steps.Contains(point))
            {
                var copy = new HashSet<Point>(shortest.Steps)
                {
                    point
                };
                paths.Add(new Path
                {
                    Head = point,
                    Steps = copy
                });
            }
        }
    }
}