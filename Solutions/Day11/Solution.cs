namespace Solutions.Day11;

public static class Solution
{
    public static int Day => 11;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var monkeys = Parse(data);

        for (var i = 0; i < 20; i++)
        {
            for (var m = 0; m < monkeys.Count; m++)
            {
                var monkey = monkeys[m];
                
                foreach (var item in monkey.Items)
                {
                    var worry = monkey.Operation(item) / 3;
                    var nextMonkey = monkey.Test(worry);
                    monkeys[nextMonkey].Items.Add(worry);
                }

                monkey.Inspections += monkey.Items.Count;
                monkey.Items = new List<long>();
            }
        }

        return monkeys
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .Select(m => m.Inspections)
            .Aggregate((x, y) => x * y)
            .ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var monkeys = Parse(data);

        for (var i = 0; i < 10000; i++)
        {
            for (var m = 0; m < monkeys.Count; m++)
            {
                var monkey = monkeys[m];
                
                foreach (var item in monkey.Items)
                {
                    var worry = monkey.Operation(item);
                    var nextMonkey = monkey.Test(worry);
                    monkeys[nextMonkey].Items.Add(worry);
                }

                monkey.Inspections += monkey.Items.Count;
                monkey.Items = new List<long>();
            }
        }

        return monkeys
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .Select(m => m.Inspections)
            .Aggregate((x, y) => x * y)
            .ToString();
    }

    private static List<Monkey> Parse(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        var monkeys = new List<Monkey>();
        var tempMonkey = new Monkey();
        
        for (var i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].StartsWith("Monkey"))
            {
                tempMonkey = new Monkey
                {
                    Id = int.Parse(dataList[i].Split(' ')[1].Replace(":", ""))
                };
                monkeys.Add(tempMonkey);
            }

            if (dataList[i].StartsWith("  Starting items:"))
            {
                var items = dataList[i]
                    .Replace("  Starting items: ", "")
                    .Split(", ")
                    .Select(long.Parse)
                    .ToList();
                tempMonkey.Items = items;
            }

            if (dataList[i].StartsWith("  Operation:"))
            {
                var operation = dataList[i].Replace("  Operation: new = ", "");
                if (operation.Contains('+'))
                {
                    var parts = operation.Split(" + ");
                    if (int.TryParse(parts[1], out var value))
                    {
                        tempMonkey.Operation = x => x + value;
                    }
                    else
                    {
                        tempMonkey.Operation = x => x + x;
                    }
                }
                else
                {
                    var parts = operation.Split(" * ");
                    if (int.TryParse(parts[1], out var value))
                    {
                        tempMonkey.Operation = x => x * value;
                    }
                    else
                    {
                        tempMonkey.Operation = x => x * x;
                    }
                }
            }

            if (dataList[i].StartsWith("  Test:"))
            {
                var divisor = dataList[i]
                    .Replace("  Test: divisible by ", "")
                    .Split()
                    .Select(int.Parse)
                    .FirstOrDefault();
                var trueMonkey = dataList[i + 1]
                    .Replace("    If true: throw to monkey ", "")
                    .Split()
                    .Select(int.Parse)
                    .FirstOrDefault();
                var falseMonkey = dataList[i + 2]
                    .Replace("    If false: throw to monkey ", "")
                    .Split()
                    .Select(int.Parse)
                    .FirstOrDefault();
                tempMonkey.Test = x => x % divisor == 0 ? trueMonkey : falseMonkey;
            }
        }

        return monkeys;
    }
}