using System.Text.RegularExpressions;
using AdventOfCode.Data;

namespace AdventOfCode.Day8;

public static class Day8Solver
{
    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(8)
            .Select(s=>s.Split(" | ")[1])
            .SelectMany(s=>s.Split(" "))
            .ToList();

        return data.Count(s=>new[]{2,3,4,7}.Contains(s.Length));

    }

    public static double SolvePuzzle2()
    {
        var data = Regex
            .Split(DataLoader.LoadDataPerLineFromDay(7).Single(), @"\D+")
            .Select(int.Parse)
            .ToList();

        var originalInput = new Dictionary<int, string>()
        {
            { 0, "abcefg" },
            { 1, "cf" },
            { 2, "acdeg" },
            { 3, "acdfg" },
            { 4, "bcdf" },
            { 5, "abdfg" },
            { 6, "abdefg" },
            { 7, "acf" },
            { 8, "abcdefg" },
            { 9, "abcdfg" },
        };

        var inputGroupedPerInputLength = new Dictionary<int, string>()
        {
            { 2, "cf" },
            { 3, "acf" },
            { 4, "bcdf" },
            { 5, "acdeg acdfg abdfg " },
            { 6, "abcefg abdefg abcdfg" },
            { 7, "abcdefg" }
        };


        return 0;
    }
}