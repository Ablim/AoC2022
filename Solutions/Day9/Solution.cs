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
                Column = 0,
                Name = i.ToString()
            });
        }
        var tailPositions = new HashSet<string>();

        foreach (var move in data)
        {
            var parts = move.Split(' ');
            var direction = parts[0];
            var steps = int.Parse(parts[1]);
            
            // Console.WriteLine(move);
            
            while (steps > 0)
            {
                MoveHead(snake[0], direction);

                for (var i = 0; i < 9; i++)
                {
                    Follow(snake[i], snake[i + 1]);
                }
                
                if (!tailPositions.Contains($"{snake[9].Row}{snake[9].Column}"))
                    tailPositions.Add($"{snake[9].Row}{snake[9].Column}");
                
                steps--;
            }
            
            // Print(snake);
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
    
    private static void Follow(Knot head, Knot tail)
    {
        var distance = Math.Sqrt(Math.Pow(head.Row - tail.Row, 2) + Math.Pow(head.Column - tail.Column, 2));
        var step = (int)(distance / 2);

        if (head.Row > tail.Row)
        {
            tail.Row += step;
        }
        else if (head.Row < tail.Row)
        {
            tail.Row -= step;
        }

        if (head.Column > tail.Column)
        {
            tail.Column += step;
        }
        else if (head.Column < tail.Column)
        {
            tail.Column -= step;
        }
    }

    private static void Print(List<Knot> snake)
    {
        for (var r = 20; r > -20; r--)
        {
            for (var c = -20; c < 20; c++)
            {
                var maybeKnot = snake.FirstOrDefault(s => s.Row == r && s.Column == c);
                if (maybeKnot != null)
                    Console.Write(maybeKnot.Name);
                else if (r == 0 && c == 0)
                    Console.Write("s");
                else
                    Console.Write(".");
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }
}