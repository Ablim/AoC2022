using Solutions.Day8;

namespace Tests.Day8;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day8/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("21", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day8/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("8", result);
    }
}