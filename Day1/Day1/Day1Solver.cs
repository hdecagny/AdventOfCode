namespace AdventOfCode.Day1;

public static class Day1Solver
{
    public static long SolveDay1Puzzle1()
    {
        var data = LoadData();

        var substraction = data.Zip(data.Skip(1), (x, y) => y - x);

        return substraction.Count(p => p > 0);
    }

    public static long SolveDay1Puzzle2()
    {
        var data = LoadData();

        var sumOfThreeElements = (data.Zip(data.Skip(1), (x, y) => x + y)).Zip(data.Skip(2), (x, y) => x + y).ToList();
        var substraction = sumOfThreeElements.Zip(sumOfThreeElements.Skip(1), (x, y) => y - x);

        return substraction.Count(p => p > 0);
    }

    private static List<int> LoadData()
    {
        return System.IO.File
            .ReadAllLines(@"C:\Users\Hcagny\source\repos\Day1\Day1\Data\Day1Data.txt")
            .Select(int.Parse)
            .ToList();
    }
}