namespace AdventOfCode2024;

using System.Numerics;

public abstract class SolverBase2024
{
    public string[] LoadDataPerLineFromDay(int day)
    {
        return File.ReadAllLines(@$"D:\Repos\AdventOfCode\AdventOfCode2024\Data\Day{day}Data.txt");
    }

    public string LoadDataFromDay(int day)
    {
        return File.ReadAllText(@$"D:\Repos\AdventOfCode\AdventOfCode2024\Data\Day{day}Data.txt");
    }

    public Dictionary<Complex, char> LoadDataAsMapFromDay(int day)
    {
        var input = LoadDataPerLineFromDay(day);

        return Enumerable.Range(0, input.Length)
            .SelectMany(y => Enumerable.Range(0, input[0].Length)
                .Select(x => new KeyValuePair<Complex, char>(-Complex.ImaginaryOne * y + x, input[y][x])))
            .ToDictionary();
    }

    public abstract double SolvePuzzle1();

    public abstract double SolvePuzzle2();
}