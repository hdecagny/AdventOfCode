namespace AdventOfCode2024;

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

    public abstract double SolvePuzzle1();

    public abstract double SolvePuzzle2();
}