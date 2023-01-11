namespace Solutions.Day11;

public class Monkey
{
    public int Id { get; init; }
    public List<long> Items { get; set; } = new();
    public Func<long, long> Operation { get; set; } = _ => 0;
    public Func<long, int> Test { get; set; } = _ => 0;
    public long Inspections { get; set; }
}