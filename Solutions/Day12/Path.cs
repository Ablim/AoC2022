namespace Solutions.Day12;

public class Path : IComparable
{
    public int Length => Steps.Count;
    public (int row, int column) Head { get; set; }
    public HashSet<(int row, int column)> Steps { get; set; } = new();
    
    public int CompareTo(object? obj)
    {
        var other = obj as Path ?? throw new InvalidCastException();
        return Length <= other.Length ? -1 : 1;
    }
}