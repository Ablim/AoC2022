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
            .Select(x => GetShapeScore(GetPlannedShape(x.Item1, x.Item2)) + GetPlannedMatchScore(x.Item2))
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

    private static char GetPlannedShape(char opponent, char outcome) =>
        (opponent, outcome) switch
        {
            ('A', 'X') => 'Z', // Rock loss
            ('A', 'Y') => 'X', // Rock draw
            ('A', 'Z') => 'Y', // Rock win
            ('B', 'X') => 'X', // Paper loss
            ('B', 'Y') => 'Y', // Paper draw
            ('B', 'Z') => 'Z', // Paper win
            ('C', 'X') => 'Y', // Scissor loss
            ('C', 'Y') => 'Z', // Scissor draw
            ('C', 'Z') => 'X', // Scissor win
            (_, _) => throw new InvalidOperationException()
        };
}