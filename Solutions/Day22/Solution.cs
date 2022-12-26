namespace Solutions.Day22;

public static class Solution
{
    public static int Day => 22;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        var mapRaw = dataList
            .TakeWhile(row => !string.IsNullOrEmpty(row))
            .ToList();
        var width = mapRaw.Max(row => row.Length);
        var height = mapRaw.Count;
        var map = new char[height, width];
        var startColumn = -1;

        for (var row = 0; row < height; row++)
        {
            var rowData = mapRaw[row].ToCharArray();
            for (var col = 0; col < rowData.Length; col++)
            {
                if (rowData[col] == ' ')
                    continue;

                if (startColumn < 0 && rowData[col] == '.')
                    startColumn = col;
                
                map[row, col] = rowData[col];
            }
        }
        
        var steps = dataList
            .Last()
            .ToList();
        var direction = Direction.Right;
        var currentRow = 0;
        var currentColumn = startColumn;
        
        while (steps.Any())
        {
            var stepCountParts = steps
                .TakeWhile(s => s != 'R' && s != 'L')
                .ToArray();
            steps.RemoveRange(0, stepCountParts.Length);
            var stepCount = int.Parse(new string(stepCountParts));
            
            // Move
            for (var i = 0; i < stepCount; i++)
            {
                switch (direction)
                {
                    case Direction.Right:
                        var rightColumn = 
                            currentColumn + 1 < width && map[currentRow, currentColumn + 1].IsVoid() 
                            || currentColumn + 1 >= width 
                                ? LoopAroundRight(map, currentRow, width) 
                                : currentColumn + 1;
                        
                        if (map[currentRow, rightColumn] == '.')
                            currentColumn = rightColumn;

                        break;
                    case Direction.Down:
                        var downRow = 
                            currentRow + 1 < height && map[currentRow + 1, currentColumn].IsVoid()
                            || currentRow + 1 >= height 
                                ? LoopAroundDown(map, currentColumn, height) 
                                : currentRow + 1;
                        
                        if (map[downRow, currentColumn] == '.')
                            currentRow = downRow;
                        
                        break;
                    case Direction.Left:
                        var leftColumn = 
                            currentColumn - 1 >= 0 && map[currentRow, currentColumn - 1].IsVoid() 
                            || currentColumn - 1 < 0 
                                ? LoopAroundLeft(map, currentRow, width) 
                                : currentColumn - 1;
                        
                        if (map[currentRow, leftColumn] == '.')
                            currentColumn = leftColumn;
                        
                        break;
                    case Direction.Up:
                        var upRow = 
                            currentRow - 1 >= 0 && map[currentRow - 1, currentColumn].IsVoid() 
                            || currentRow - 1 < 0 
                                ? LoopAroundUp(map, currentColumn, height) 
                                : currentRow - 1;
                        
                        if (map[upRow, currentColumn] == '.')
                            currentRow = upRow;
                        
                        break;
                    default:
                        break;
                }
            }

            // Rotate
            if (steps.FirstOrDefault() == 'R')
            {
                direction = Rotate(direction, Direction.Right);
                steps.RemoveAt(0);
            }

            if (steps.FirstOrDefault() == 'L')
            {
                direction = Rotate(direction, Direction.Left);
                steps.RemoveAt(0);
            }
        }
        
        return (1000 * (currentRow + 1) 
                + 4 * (currentColumn + 1) 
                + direction).ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "";
    }
    
    private static int LoopAroundRight(char[,] map, int currentRow, int width)
    {
        var result = 0;
        for (var i = 0; i < width; i++)
        {
            if (!map[currentRow, i].IsVoid())
            {
                result = i;
                break;
            }
        }

        return result;
    }
    
    private static int LoopAroundLeft(char[,] map, int currentRow, int width)
    {
        var result = width - 1;
        for (var i = width - 1; i >= 0; i--)
        {
            if (!map[currentRow, i].IsVoid())
            {
                result = i;
                break;
            }
        }

        return result;
    }
    
    private static int LoopAroundUp(char[,] map, int currentColumn, int height)
    {
        var result = height - 1;
        for (var i = height - 1; i >= 0; i--)
        {
            if (!map[i, currentColumn].IsVoid())
            {
                result = i;
                break;
            }
        }

        return result;
    }
    
    private static int LoopAroundDown(char[,] map, int currentColumn, int height)
    {
        var result = 0;
        for (var i = 0; i < height; i++)
        {
            if (!map[i, currentColumn].IsVoid())
            {
                result = i;
                break;
            }
        }

        return result;
    }

    private static Direction Rotate(Direction direction, Direction rotation) =>
        (direction, rotation) switch
        {
            (Direction.Right, Direction.Right) => Direction.Down,
            (Direction.Down, Direction.Right) => Direction.Left,
            (Direction.Left, Direction.Right) => Direction.Up,
            (Direction.Up, Direction.Right) => Direction.Right,
            (Direction.Right, Direction.Left) => Direction.Up,
            (Direction.Down, Direction.Left) => Direction.Right,
            (Direction.Left, Direction.Left) => Direction.Down,
            (Direction.Up, Direction.Left) => Direction.Left,
            _ => throw new InvalidOperationException()
        };

    private enum Direction
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3
    }

    private static bool IsVoid(this char c) => c is ' ' or '\0';
}