using AdventOfCode.Data;

namespace AdventOfCode.Day2;

public static class Day2Solver
{
    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(2)
            .Select(s => s.Split(' '))
            .ToList();

        var depth = 0;
        var width = 0;

        foreach (var line in data)
        {
            switch (line[0])
            {
                case "forward":
                    width += int.Parse(line[1]);
                    break;
                case "up":
                    depth -= int.Parse(line[1]);
                    break;
                case "down":
                    depth += int.Parse(line[1]);
                    break;
            }
        }

        return depth * width;
    }

    public static long SolvePuzzle2()
    {
        var data = DataLoader.LoadDataPerLineFromDay(2)
            .Select(s => s.Split(' '))
            .ToList();

        var depth = 0;
        var width = 0;
        var aim = 0;

        foreach (var line in data)
        {
            switch (line[0])
            {
                case "forward":
                    width += int.Parse(line[1]);
                    depth += aim * int.Parse(line[1]);
                    break;
                case "up":
                    aim -= int.Parse(line[1]);
                    break;
                case "down":
                    aim += int.Parse(line[1]);
                    break;
            }
        }

        return depth * width;
    }
}