namespace Solutions.Day13;

public static class Solution
{
    public static int Day => 13;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var dataList = data.ToList();
        var pairs = new List<(int, string, string)>();
        var index = 1;

        while (dataList.Any())
        {
            if (dataList.First() == "")
            {
                dataList.RemoveAt(0);
                continue;
            }
            
            var pair = dataList.Take(2).ToList();
            pairs.Add((index++, pair[0], pair[1]));
            dataList.RemoveRange(0, 2);
        }

        return pairs.Select(p => (p.Item1, AreInOrder(p.Item2, p.Item3)))
            .Where(x => x.Item2 == 1)
            .Sum(x => x.Item1)
            .ToString();
    }

    private static int AreInOrder(string left, string right)
    {
        if (int.TryParse(left, out var leftNum) && int.TryParse(right, out var rightNum))
            return leftNum < rightNum 
                ? 1 
                : leftNum > rightNum 
                    ? -1 
                    : 0;
        
        if (int.TryParse(left, out _))
            return AreInOrder($"[{left}]", right);
        
        if (int.TryParse(right, out _))
            return AreInOrder(left, $"[{right}]");

        var leftList = left.Take(new Range(1, left.Length - 1))
            .ParseList()
            .ToList();
        var rightList = right.Take(new Range(1, right.Length - 1))
            .ParseList()
            .ToList();

        while (leftList.Any() && rightList.Any())
        {
            var order = AreInOrder(leftList.First(), rightList.First());
            if (order == 0)
            {
                leftList.RemoveAt(0);
                rightList.RemoveAt(0);
            }
            else
            {
                return order;
            }
        }
        
        return leftList.Count < rightList.Count
            ? 1
            : leftList.Count > rightList.Count 
                ? -1
                : 0;
    }

    private static IEnumerable<string> ParseList(this IEnumerable<char> source)
    {
        var result = new List<string>();
        var buffer = new List<char>();
        var level = 0;

        foreach (var item in source)
        {
            if (level == 0)
            {
                if (int.TryParse(item.ToString(), out _))
                {
                    buffer.Add(item);
                }
                else if (item == '[')
                {
                    buffer.Add(item);
                    level++;
                }
                else if (item == ',')
                {
                    result.Add(new string(buffer.ToArray()));
                    buffer = new List<char>();
                }
            }
            else
            {
                if (item == '[')
                    level++;
                
                if (item == ']')
                    level--;
                
                buffer.Add(item);
            }
        }
        
        result.Add(new string(buffer.ToArray()));
        result.RemoveAll(string.IsNullOrEmpty);
        return result;
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var itemList = data.ToList();
        itemList.RemoveAll(string.IsNullOrEmpty);
        itemList.Add("[[2]]");
        itemList.Add("[[6]]");

        var sortedList = new List<string>();

        while (itemList.Any())
        {
            var itemToRemove = string.Empty;
            
            foreach (var item in itemList)
            {
                var isSorted = true;
                
                foreach (var otherItem in itemList)
                {
                    if (item == otherItem)
                        continue;

                    if (AreInOrder(item, otherItem) == -1)
                    {
                        isSorted = false;
                        break;
                    }
                }

                if (isSorted)
                {
                    sortedList.Add(item);
                    itemToRemove = item;
                    break;
                }
            }

            itemList.Remove(itemToRemove);
        }

        var indexA = 0;
        var indexB = 0;
        for (var i = 0; i < sortedList.Count; i++)
        {
            if (sortedList[i] == "[[2]]")
                indexA = i + 1;
            
            if (sortedList[i] == "[[6]]")
                indexB = i + 1;
        }
        
        return (indexA * indexB).ToString();
    }
}