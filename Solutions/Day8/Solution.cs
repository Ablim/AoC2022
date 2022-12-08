namespace Solutions.Day8;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data)
    {
        var trees = data
            .Select(row => row.ToCharArray())
            .Select(row => row.Select(tree => int.Parse($"{tree}")).ToArray())
            .ToArray();
        var visibleCount = 0;

        for (var row = 0; row < trees.Length; row++)
        {
            for (var col = 0; col < trees[0].Length; col++)
            {
                visibleCount += IsVisible(trees, row, col);
            }
        }
        
        return visibleCount.ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        var trees = data
            .Select(row => row.ToCharArray())
            .Select(row => row.Select(tree => int.Parse($"{tree}")).ToArray())
            .ToArray();
        var highScore = 0;

        for (var row = 0; row < trees.Length; row++)
        {
            for (var col = 0; col < trees[0].Length; col++)
            {
                var score = GetScenicScore(trees, row, col);
                if (score > highScore)
                    highScore = score;
            }
        }
        
        return highScore.ToString();
    }

    private static int IsVisible(int[][] trees, int row, int col)
    {
        var height = trees[row][col];
        var rows = trees.Length;
        var cols = trees[0].Length;
        var visibleUp = true;
        var visibleDown = true;
        var visibleRight = true;
        var visibleLeft = true;
        
        // Up
        for (var r = row - 1; r >= 0; r--)
        {
            if (trees[r][col] >= height)
            {
                visibleUp = false;
                break;
            }
        }

        if (visibleUp)
            return 1;
        
        // Down
        for (var r = row + 1; r < rows; r++)
        {
            if (trees[r][col] >= height)
            {
                visibleDown = false;
                break;
            }
        }
        
        if (visibleDown)
            return 1;
        
        // Right
        for (var c = col + 1; c < cols; c++)
        {
            if (trees[row][c] >= height)
            {
                visibleRight = false;
                break;
            }
        }
        
        if (visibleRight)
            return 1;
        
        // Left
        for (var c = col - 1; c >= 0; c--)
        {
            if (trees[row][c] >= height)
            {
                visibleLeft = false;
                break;
            }
        }
        
        return visibleLeft ? 1 : 0;
    }
    
    private static int GetScenicScore(int[][] trees, int row, int col)
    {
        var height = trees[row][col];
        var rows = trees.Length;
        var cols = trees[0].Length;
        var visibleUp = 0;
        var visibleDown = 0;
        var visibleRight = 0;
        var visibleLeft = 0;
        
        // Up
        for (var r = row - 1; r >= 0; r--)
        {
            visibleUp++;
            if (trees[r][col] >= height)
                break;
        }

        // Down
        for (var r = row + 1; r < rows; r++)
        {
            visibleDown++;
            if (trees[r][col] >= height)
                break;
        }
        
        // Right
        for (var c = col + 1; c < cols; c++)
        {
            visibleRight++;
            if (trees[row][c] >= height)
                break;
        }
        
        // Left
        for (var c = col - 1; c >= 0; c--)
        {
            visibleLeft++;
            if (trees[row][c] >= height)
                break;
        }
        
        return visibleUp * visibleDown * visibleRight * visibleLeft;
    }
}