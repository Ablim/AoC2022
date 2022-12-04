using Solutions.Day4;

namespace Tests.Day4;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day4/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("2", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day4/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("4", result);
    }
}