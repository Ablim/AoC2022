using Solutions.Day3;

namespace Tests.Day3;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day3/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("157", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day3/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("70", result);
    }
}