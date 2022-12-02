namespace Solutions.Day2;

public static class Solution
{
    public static string SolvePart1(IEnumerable<string> data)
    {
        return data.Select(GetScore)
            .Sum()
            .ToString();
    }

    public static string SolvePart2(IEnumerable<string> data)
    {
        return data.Select(x => x.Split(' '))
            .Select(x => (char.Parse(x[0]), char.Parse(x[1])))
            .Select(x => (x.Item1, GetPlannedMatchScore(x.Item2)))
            .Select(x => (GetShapeScore(GetPlannedShape(x.Item1, x.Item2)), x.Item2))
            .Select(x => x.Item1 + x.Item2)
            .Sum()
            .ToString();
    }

    private static int GetScore(string plan)
    {
        var choices = plan.Split(' ')
            .Select(char.Parse)
            .ToArray();
        return GetMatchScore(choices[0], choices[1]) + GetShapeScore(choices[1]);
    }

    /*
     * A -> Rock
     * B -> Paper
     * C -> Scissors
     * X -> Rock
     * Y -> Paper
     * Z -> Scissors
     *
     * Rock -> 1
     * Paper -> 2
     * Scissors -> 3
     * 
     * Loss -> 0
     * Draw -> 3
     * Win -> 6
     */
    private static int GetMatchScore(char opponent, char me) =>
        (opponent, me) switch
        {
            ('A', 'X') => 3, // Draw
            ('A', 'Y') => 6, // Win
            ('A', 'Z') => 0, // Loss
            ('B', 'X') => 0, // Loss
            ('B', 'Y') => 3, // Draw
            ('B', 'Z') => 6, // Win
            ('C', 'X') => 6, // Win
            ('C', 'Y') => 0, // Loss
            ('C', 'Z') => 3, // Draw
            (_, _) => throw new InvalidOperationException()
        };
    
    private static int GetPlannedMatchScore(char plan) =>
        plan switch
        {
            'X' => 0,
            'Y' => 3,
            'Z' => 6,
            _ => throw new InvalidOperationException()
        };

    private static int GetShapeScore(char sign) =>
        sign switch
        {
            'X' => 1,
            'Y' => 2,
            'Z' => 3,
            _ => throw new InvalidOperationException()
        };

    private static char GetPlannedShape(char opponent, int matchScore) =>
        (opponent, matchScore) switch
        {
            ('A', 0) => 'Z', // Rock loss
            ('A', 3) => 'X', // Rock draw
            ('A', 6) => 'Y', // Rock win
            ('B', 0) => 'X', // Paper loss
            ('B', 3) => 'Y', // Paper draw
            ('B', 6) => 'Z', // Paper win
            ('C', 0) => 'Y', // Scissor loss
            ('C', 3) => 'Z', // Scissor draw
            ('C', 6) => 'X', // Scissor win
            (_, _) => throw new InvalidOperationException()
        };
}