namespace AdventOfCode2023;

public abstract class SolverBase2023
{
    public string[] LoadDataPerLineFromDay(int day)
    {
        return System.IO.File.ReadAllLines(@$"D:\Repos\AdventOfCode\AdventOfCode2023\Data\Day{day}Data.txt");
    }

    public string LoadDataFromDay(int day)
    {
        return System.IO.File.ReadAllText(@$"D:\Repos\AdventOfCode\AdventOfCode2023\Data\Day{day}Data.txt");
    }

    public abstract double SolvePuzzle1();
    
    public abstract double SolvePuzzle2();
}