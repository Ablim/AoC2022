using Solutions.Day1;

namespace Tests.Day1;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day1/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("24000", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day1/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("45000", result);
    }
}