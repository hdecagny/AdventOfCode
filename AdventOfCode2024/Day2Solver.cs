namespace AdventOfCode2024;

public class Day2Solver : SolverBase2024
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataPerLineFromDay(2)
            .Select(l => l.Split(' ').Select(int.Parse).ToList())
            .ToList();

        return data.Where(IsSafe).Count();
    }

    private static bool IsSafe(List<int> input)
    {
        var comparingWithNext = input.Zip(input.Skip(1)).ToList();
        var isAlwaysIncreasing = comparingWithNext.All(t => t.Item1 > t.Item2);
        var isAlwaysDecreasing = comparingWithNext.All(t => t.Item1 < t.Item2);
        var differenceAlwaysContained = comparingWithNext.All(t => Math.Abs(t.Item1 - t.Item2) < 4);

        return (isAlwaysDecreasing || isAlwaysIncreasing) && differenceAlwaysContained;
    }

    public override double SolvePuzzle2()
    {
        var data = LoadDataPerLineFromDay(2)
            .Select(l => l.Split(' ').Select(int.Parse).ToList())
            .ToList();

        return data.Where(IsSafeWithoutPossiblyOneLevel).Count();
    }

    private static bool IsSafeWithoutPossiblyOneLevel(List<int> input)
    {
        if (IsSafe(input))
        {
            return true;
        }

        var range = Enumerable.Range(0, input.Count).ToList();
        return range
            .Select(t =>
            {
                var trimmedList = input.ToList();
                trimmedList.RemoveAt(t);
                return trimmedList;
            })
            .Any(IsSafe);
    }
}