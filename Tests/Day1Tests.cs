using Solutions.Day1;

namespace Tests;

public class Day1Tests
{
    [Fact]
    public async Task Part1()
    {
        var data = await File.ReadAllLinesAsync("Data/Day1.txt");
        var result = Solution.SolvePart1(data);
        Assert.Equal("24000", result);
    }
    
    [Fact]
    public async Task Part2()
    {
        var data = await File.ReadAllLinesAsync("Data/Day1.txt");
        var result = Solution.SolvePart2(data);
        Assert.Equal("45000", result);
    }
}