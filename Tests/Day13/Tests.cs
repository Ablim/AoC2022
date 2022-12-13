using Solutions.Day13;

namespace Tests.Day13;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("13", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("140", result);
    }
}