namespace Solutions.Day9;

public static class Solution
{
    public static int Day => 9;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        // Use record
        (int row, int col) head = (0, 0);
        (int row, int col) tail = (0, 0);
        var tailPositions = new List<(int, int)>
        {
            tail
        };

        foreach (var move in data)
        {
            var parts = move.Split(' ');
            var steps = int.Parse(parts[1]);
            
            while (steps > 0)
            {
                (head, tail) = Move(head, tail, parts[0]);
                
                if (!tailPositions.Contains(tail))
                    tailPositions.Add(tail);
                
                steps--;
            }
        }
        
        return tailPositions.Count.ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return "0";
    }

    private static ((int, int), (int, int)) Move((int row, int col) head, (int row, int col) tail, string direction)
    {
        switch (direction)
        {
            case "U":
                head.row++;
                
                if (head.row == tail.row + 2)
                {
                    tail.row++;
                    
                    if (head.col == tail.col - 1)
                        tail.col--;
                    else if (head.col == tail.col + 1)
                        tail.col++;
                }
                
                break;
            case "D":
                head.row--;
                
                if (head.row == tail.row - 2)
                {
                    tail.row--;
                    
                    if (head.col == tail.col - 1)
                        tail.col--;
                    else if (head.col == tail.col + 1)
                        tail.col++;
                }
                
                break;
            case "R":
                head.col++;
                
                if (head.col == tail.col + 2)
                {
                    tail.col++;
                    
                    if (head.row == tail.row - 1)
                        tail.row--;
                    else if (head.row == tail.row + 1)
                        tail.row++;
                }
                
                break;
            case "L":
                head.col--;
                
                if (head.col == tail.col - 2)
                {
                    tail.col--;
                    
                    if (head.row == tail.row - 1)
                        tail.row--;
                    else if (head.row == tail.row + 1)
                        tail.row++;
                }
                
                break;
            default:
                throw new InvalidOperationException();
        }
        
        return (head, tail);
    }
}