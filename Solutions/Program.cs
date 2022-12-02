using Solutions.Day2;

Console.WriteLine("*** Advent of Code 2022 ***");

// var client = new HttpClient();
// var response = await client.GetAsync("https://adventofcode.com/2022/day/1/input");
// var data = await response.Content.ReadAsStringAsync();

var data = await File.ReadAllLinesAsync("Day2/Data.txt");

var part1 = Solution.SolvePart1(data);
var part2 = Solution.SolvePart2(data);

Console.WriteLine("Part 1");
Console.WriteLine(part1);
Console.WriteLine();
Console.WriteLine("Part 1");
Console.WriteLine(part2);