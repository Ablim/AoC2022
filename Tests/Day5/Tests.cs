using Solutions.Day5;

namespace Tests.Day5;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day5/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("CMZ", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day5/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("MCD", result);
    }
}