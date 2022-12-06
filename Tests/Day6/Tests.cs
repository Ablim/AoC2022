using Solutions.Day6;

namespace Tests.Day6;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day6/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("7", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day6/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("19", result);
    }
}