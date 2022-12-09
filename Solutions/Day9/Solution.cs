namespace Solutions.Day9;

public static class Solution
{
    public static int Day => 9;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var head = new Knot
        {
            Row = 0,
            Column = 0
        };
        var tail = new Knot
        {
            Row = 0,
            Column = 0
        };
        var tailPositions = new HashSet<string>();

        foreach (var move in data)
        {
            var parts = move.Split(' ');
            var steps = int.Parse(parts[1]);
            
            while (steps > 0)
            {
                Move(head, tail, parts[0]);
                
                if (!tailPositions.Contains($"{tail.Row}{tail.Column}"))
                    tailPositions.Add($"{tail.Row}{tail.Column}");
                
                steps--;
            }
        }
        
        return tailPositions.Count.ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var snake = new List<Knot>();
        for (var i = 0; i < 10; i++)
        {
            snake.Add(new Knot
            {
                Row = 0,
                Column = 0
            });
        }
        var tailPositions = new HashSet<string>();

        foreach (var move in data)
        {
            var parts = move.Split(' ');
            var direction = parts[0];
            var steps = int.Parse(parts[1]);
            
            while (steps > 0)
            {
                Console.WriteLine($"Move head {direction}");
                MoveHead(snake[0], direction);

                for (var i = 0; i < 9; i++)
                {
                    Follow(snake[i], snake[i + 1], i + 1);
                }
                
                if (!tailPositions.Contains($"{snake[9].Row}{snake[9].Column}"))
                    tailPositions.Add($"{snake[9].Row}{snake[9].Column}");
                
                steps--;
            }
        }
        
        return tailPositions.Count.ToString();
    }

    private static void Move(Knot head, Knot tail, string direction)
    {
        switch (direction)
        {
            case "U":
                head.Row++;
                
                if (head.Row == tail.Row + 2)
                {
                    tail.Row++;
                    
                    if (head.Column == tail.Column - 1)
                        tail.Column--;
                    else if (head.Column == tail.Column + 1)
                        tail.Column++;
                }
                
                break;
            case "D":
                head.Row--;
                
                if (head.Row == tail.Row - 2)
                {
                    tail.Row--;
                    
                    if (head.Column == tail.Column - 1)
                        tail.Column--;
                    else if (head.Column == tail.Column + 1)
                        tail.Column++;
                }
                
                break;
            case "R":
                head.Column++;
                
                if (head.Column == tail.Column + 2)
                {
                    tail.Column++;
                    
                    if (head.Row == tail.Row - 1)
                        tail.Row--;
                    else if (head.Row == tail.Row + 1)
                        tail.Row++;
                }
                
                break;
            case "L":
                head.Column--;
                
                if (head.Column == tail.Column - 2)
                {
                    tail.Column--;
                    
                    if (head.Row == tail.Row - 1)
                        tail.Row--;
                    else if (head.Row == tail.Row + 1)
                        tail.Row++;
                }
                
                break;
            default:
                throw new InvalidOperationException();
        }
    }
    
    private static void MoveHead(Knot head, string direction)
    {
        switch (direction)
        {
            case "U":
                head.Row++;
                break;
            case "D":
                head.Row--;
                break;
            case "R":
                head.Column++;
                break;
            case "L":
                head.Column--;
                break;
            default:
                throw new InvalidOperationException();
        }
    }
    
    private static void Follow(Knot head, Knot tail, int tailIndex)
    {
        if (head.Row == tail.Row + 2)
        {
            tail.Row++;
            Console.WriteLine($"{tailIndex} move up");

            if (head.Column == tail.Column - 1)
            {
                tail.Column--;
                Console.WriteLine($"{tailIndex} move left");
            }
            else if (head.Column == tail.Column + 1)
            {
                tail.Column++;
                Console.WriteLine($"{tailIndex} move right");
            }
        }
        else if (head.Row == tail.Row - 2)
        {
            tail.Row--;
            Console.WriteLine($"{tailIndex} move down");

            if (head.Column == tail.Column - 1)
            {
                tail.Column--;
                Console.WriteLine($"{tailIndex} move left");
            }
            else if (head.Column == tail.Column + 1)
            {
                tail.Column++;
                Console.WriteLine($"{tailIndex} move right");
            }
        }
        else if (head.Column == tail.Column + 2)
        {
            tail.Column++;
            Console.WriteLine($"{tailIndex} move right");

            if (head.Row == tail.Row - 1)
            {
                tail.Row--;
                Console.WriteLine($"{tailIndex} move down");
            }
            else if (head.Row == tail.Row + 1)
            {
                tail.Row++;
                Console.WriteLine($"{tailIndex} move up");
            }
        }
        else if (head.Column == tail.Column - 2)
        {
            tail.Column--;
            Console.WriteLine($"{tailIndex} move left");

            if (head.Row == tail.Row - 1)
            {
                tail.Row--;
                Console.WriteLine($"{tailIndex} move down");
            }
            else if (head.Row == tail.Row + 1)
            {
                tail.Row++;
                Console.WriteLine($"{tailIndex} move up");
            }
        }
    }
}