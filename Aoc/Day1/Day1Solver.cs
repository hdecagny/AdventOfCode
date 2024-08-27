

namespace AdventOfCode.Day1;

public static class Day1Solver
{
    public static long SolveDay1Puzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(1)
            .Select(int.Parse)
            .ToList(); 

        var substraction = data.Zip(data.Skip(1), (x, y) => y - x);

        return substraction.Count(p => p > 0);
    }

    public static long SolveDay1Puzzle2()
    {
        var data = DataLoader.LoadDataPerLineFromDay(1)
            .Select(int.Parse)
            .ToList(); 

        //Probably not efficient if i had to work with Gigabites of data but it has the merit to hold in two lines

        var sumOfThreeElements = data
            .Zip(data.Skip(1), (x, y) => x + y)
            .Zip(data.Skip(2), (x, y) => x + y)
            .ToList();

        var substraction = sumOfThreeElements
            .Zip(sumOfThreeElements.Skip(1), (x, y) => y - x);

        return substraction.Count(p => p > 0);
    }
}