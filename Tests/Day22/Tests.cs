using Solutions.Day22;

namespace Tests.Day22;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("6032", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("5031", result);
    }
}