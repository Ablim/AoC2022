using Solutions.Day18;

namespace Tests.Day18;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("64", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("58", result);
    }
}