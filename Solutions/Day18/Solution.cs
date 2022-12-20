namespace Solutions.Day18;

public static class Solution
{
    public static int Day => 18;
    
    public static string SolvePart1(IEnumerable<string> data)
    {
        var cubes = Parse(data);
        var map = new HashSet<string>();
        var sides = 0;
        
        cubes.ForEach(cube =>
        {
            var neighbors = new List<string>
            {
                $"{cube.x + 1},{cube.y},{cube.z}",
                $"{cube.x - 1},{cube.y},{cube.z}",
                $"{cube.x},{cube.y + 1},{cube.z}",
                $"{cube.x},{cube.y - 1},{cube.z}",
                $"{cube.x},{cube.y},{cube.z + 1}",
                $"{cube.x},{cube.y},{cube.z - 1}"
            };

            var newSides = 6;
            neighbors.ForEach(neighbor =>
            {
                if (map.Contains(neighbor))
                    newSides -= 2;
            });
            
            var toAdd = $"{cube.x},{cube.y},{cube.z}";
            map.Add(toAdd);
            sides += newSides;
        });
        
        return sides.ToString();
    }

    private static List<(int x, int y, int z)> Parse(IEnumerable<string> data)
    {
        return data
            .Select(row => row.Split(","))
            .Select(row => row.Select(int.Parse).ToArray())
            .Select(row => (x: row[0], y: row[1], z: row[2]))
            .ToList();
    }

    // 3406 too high
    public static string SolvePart2(IEnumerable<string> data)
    {
        var cubes = Parse(data);
        var map = new HashSet<string>();
        var maybeAir = new Dictionary<string, int>();
        var sides = 0;
        
        cubes.ForEach(cube =>
        {
            var neighbors = new List<string>
            {
                $"{cube.x + 1},{cube.y},{cube.z}",
                $"{cube.x - 1},{cube.y},{cube.z}",
                $"{cube.x},{cube.y + 1},{cube.z}",
                $"{cube.x},{cube.y - 1},{cube.z}",
                $"{cube.x},{cube.y},{cube.z + 1}",
                $"{cube.x},{cube.y},{cube.z - 1}"
            };

            var newSides = 6;
            neighbors.ForEach(neighbor =>
            {
                if (map.Contains(neighbor))
                    newSides -= 2;

                if (maybeAir.ContainsKey(neighbor))
                {
                    maybeAir[neighbor]++;
                }
                else
                {
                    maybeAir.Add(neighbor, 1);
                }
            });
            
            var toAdd = $"{cube.x},{cube.y},{cube.z}";
            map.Add(toAdd);
            sides += newSides;
        });
        
        cubes.ForEach(cube =>
        {
            var key = $"{cube.x},{cube.y},{cube.z}";
            maybeAir.Remove(key);
        });

        // TODO Simulate steam and explore all cavities
        
        var hidden = maybeAir.Count(m => m.Value == 6);
        var result = sides - 6 * hidden;
        return result.ToString();
    }
}