using System.Diagnostics;
using System.Net;
using Solutions.Day22;

var inputFile = $"Input{Solution.Day}.txt";

if (!File.Exists(inputFile))
{
    var container = new CookieContainer();
    var sessionCookie = await File.ReadAllTextAsync(".adventofcode.com");
    container.Add(new Cookie("session", sessionCookie, string.Empty, ".adventofcode.com"));
    var handler = new HttpClientHandler
    {
        CookieContainer = container
    };
    
    var client = new HttpClient(handler);
    var response = await client.GetAsync($"https://adventofcode.com/2022/day/{Solution.Day}/input");
    var bytes = await response.Content.ReadAsByteArrayAsync();
    await File.WriteAllBytesAsync(inputFile, bytes);
}

var data = await File.ReadAllLinesAsync(inputFile);

Console.WriteLine("Advent of Code 2022");
Console.WriteLine($"Day {Solution.Day}");
Console.WriteLine();

var stopwatch = new Stopwatch();
stopwatch.Start();
var part1 = Solution.SolvePart1(data);
stopwatch.Stop();
Console.WriteLine($"Part 1 ({stopwatch.ElapsedMilliseconds} ms)");
Console.WriteLine(part1);
Console.WriteLine();

stopwatch.Restart();
var part2 = Solution.SolvePart2(data);
stopwatch.Stop();
Console.WriteLine($"Part 2 ({stopwatch.ElapsedMilliseconds} ms)");
Console.WriteLine(part2);