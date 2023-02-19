using Solutions.Day16;

namespace Tests.Day16;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("1651", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync($"Day{Solution.Day}/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("1651", result);
    }
}