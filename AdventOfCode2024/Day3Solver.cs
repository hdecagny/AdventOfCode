namespace AdventOfCode2024;

using System.Text.RegularExpressions;

public class Day3Solver : SolverBase2024
{
    public override double SolvePuzzle1()
    {
        var input = LoadDataFromDay(3);
        var occurences = Regex.Matches(input, @"mul\(\d{1,3},\d{1,3}\)").Select(m => m.ToString());

        var result = occurences.Select(s => Regex.Matches(s, @"\d{1,3}")
                .Select(m => int.Parse(m.ToString())).ToList())
            .Select(d => d[0] * d[1])
            .Sum();

        return result;
    }

    public override double SolvePuzzle2()
    {
        var input = LoadDataFromDay(3);
        var inputWithoutDont = Regex.Replace(input, @"don't\(\)(.|\n)+?do\(\)", "do");

        var occurences = Regex.Matches(inputWithoutDont, @"mul\(\d{1,3},\d{1,3}\)").Select(m => m.ToString());

        var result = occurences.Select(s => Regex.Matches(s, @"\d{1,3}")
                .Select(m => int.Parse(m.ToString())).ToList())
            .Select(d => d[0] * d[1])
            .Sum();

        return result;
    }
}