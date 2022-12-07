using Solutions.Day7;

namespace Tests.Day7;

public class Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Day7/Data.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("95437", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Day7/Data.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("24933642", result);
    }
}