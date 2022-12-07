namespace Solutions.Day7;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data)
    {
        var folders = Explore(data);
        return folders.Values
            .Where(s => s <= 100000)
            .Sum()
            .ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var folders = Explore(data);
        const int totalSpace = 70000000;
        const int requiredFreeSpace = 30000000;
        var spaceLeft = totalSpace - folders["/"];
        var amountToFree = requiredFreeSpace - spaceLeft;
        return folders.Where(f => f.Value >= amountToFree)
            .MinBy(f => f.Value)
            .Value
            .ToString();
    }

    private static Dictionary<string, int> Explore(IEnumerable<string> data)
    {
        var folderSizes = new Dictionary<string, int>();
        var folderWithFolders = new Dictionary<string, List<string>>();
        var path = new List<string>();
        var isList = false;

        foreach (var row in data)
        {
            if (row.StartsWith("$ cd "))
            {
                isList = false;
                var folderName = row.Replace("$ cd ", "");
                if (folderName == "..")
                {
                    path.RemoveAt(path.Count - 1);
                    continue;
                }
                
                path.Add(folderName);
                folderSizes.TryAdd(path.PrintPath(), 0);
                folderWithFolders.TryAdd(path.PrintPath(), new List<string>());
                continue;
            }

            if (row.StartsWith("$ ls"))
            {
                isList = true;
                continue;
            }

            if (isList)
            {
                var currentDir = path.PrintPath();
                var parts = row.Split(' ');
                if (parts[0] == "dir")
                {
                    folderWithFolders[currentDir].Add($"{currentDir}/{parts[1]}");
                }
                else
                {
                    var size = int.Parse(parts[0]);
                    folderSizes[currentDir] += size;
                }
            }
        }

        while (folderWithFolders.Count > 0)
        {
            foreach (var mapping in folderWithFolders)
            {
                if (mapping.Value.Any(f => folderWithFolders.ContainsKey(f)))
                    continue;
                
                folderSizes[mapping.Key] += mapping.Value
                    .Select(f => folderSizes[f])
                    .Sum();
                folderWithFolders.Remove(mapping.Key);
            }
        }

        return folderSizes;
    }

    private static string PrintPath(this IEnumerable<string> path) =>
        string.Join('/', path);
}