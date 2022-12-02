using Solutions.Day2;

namespace Tests.Day2;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day2/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("15", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day2/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("12", result);
    }
}