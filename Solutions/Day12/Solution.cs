namespace Solutions.Day12;

public static class Solution
{
    public static int Day => 12;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var map = data
            .Select(row => row.ToArray())
            .ToArray();
        var start = FindStart(map);
        var paths = new SortedSet<Path>();
        paths.Add(new Path
        {
            Head = start,
            Steps = new HashSet<(int row, int column)>
            {
                start
            }
        });

        while (paths.Any())
        {
            var shortest = paths.Min ?? throw new InvalidOperationException();
            var level = map[shortest.Head.row][shortest.Head.column];

            if (level == 'E')
                break;

            if (shortest.Head.row - 1 >= 0)
            {
                var up = map[shortest.Head.row - 1][shortest.Head.column];
                if (up.Score() >= level.Score() - 1 && up.Score() <= level.Score() + 1)
                {
                    if (!shortest.Steps.Contains((shortest.Head.row - 1, shortest.Head.column)))
                    {
                        var copy = new HashSet<(int row, int column)>(shortest.Steps)
                        {
                            (shortest.Head.row - 1, shortest.Head.column)
                        };
                        paths.Add(new Path
                        {
                            Head = (shortest.Head.row - 1, shortest.Head.column),
                            Steps = copy
                        });
                    }
                }
            }
            
            if (shortest.Head.row + 1 < map.Length)
            {
                var down = map[shortest.Head.row + 1][shortest.Head.column];
                if (down.Score() >= level.Score() - 1 && down.Score() <= level.Score() + 1)
                {
                    if (!shortest.Steps.Contains((shortest.Head.row + 1, shortest.Head.column)))
                    {
                        var copy = new HashSet<(int row, int column)>(shortest.Steps)
                        {
                            (shortest.Head.row + 1, shortest.Head.column)
                        };
                        paths.Add(new Path
                        {
                            Head = (shortest.Head.row + 1, shortest.Head.column),
                            Steps = copy
                        });
                    }
                }
            }
            
            if (shortest.Head.column - 1 >= 0)
            {
                var left = map[shortest.Head.row][shortest.Head.column - 1];
                if (left.Score() >= level.Score() - 1 && left.Score() <= level.Score() + 1)
                {
                    if (!shortest.Steps.Contains((shortest.Head.row, shortest.Head.column - 1)))
                    {
                        var copy = new HashSet<(int row, int column)>(shortest.Steps)
                        {
                            (shortest.Head.row, shortest.Head.column - 1)
                        };
                        paths.Add(new Path
                        {
                            Head = (shortest.Head.row, shortest.Head.column - 1),
                            Steps = copy
                        });
                    }
                }
            }
            
            if (shortest.Head.column + 1 < map[0].Length)
            {
                var right = map[shortest.Head.row][shortest.Head.column + 1];
                if (right.Score() >= level.Score() - 1 && right.Score() <= level.Score() + 1)
                {
                    if (!shortest.Steps.Contains((shortest.Head.row, shortest.Head.column + 1)))
                    {
                        var copy = new HashSet<(int row, int column)>(shortest.Steps)
                        {
                            (shortest.Head.row, shortest.Head.column + 1)
                        };
                        paths.Add(new Path
                        {
                            Head = (shortest.Head.row, shortest.Head.column + 1),
                            Steps = copy
                        });
                    }
                }
            }

            paths.Remove(shortest);
        }
        
        return (paths.Min?.Length - 1).ToString() ?? "0";
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }

    private static (int row, int column) FindStart(char[][] map)
    {
        for (var row = 0; row < map.Length; row++)
        {
            for (var col = 0; col < map[0].Length; col++)
            {
                if (map[row][col] == 'S')
                    return (row, col);
            }
        }

        return (0, 0);
    }

    private static int Score(this char level) =>
        level switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => level
        };
}