namespace AdventOfCode2022;

public abstract class SolverBase2022
{
    public string[] LoadDataPerLineFromDay(int day)
    {
        return System.IO.File.ReadAllLines(@$"D:\Repos\AdventOfCode - C#\AdventOfCode{2022}\Data\Day{day}Data.txt");
    }

    public string LoadDataFromDay(int day)
    {
        return System.IO.File.ReadAllText(@$"D:\Repos\AdventOfCode - C#\AdventOfCode{2022}\Data\Day{day}Data.txt");
    }
}