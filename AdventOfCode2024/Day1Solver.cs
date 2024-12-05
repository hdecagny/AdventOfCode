namespace AdventOfCode2024;

public class Day1Solver : SolverBase2024
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataPerLineFromDay(1).Select(l => l.Split("   ")).ToList();

        var leftColumn = data.Select(d => d[0]).Select(int.Parse).OrderBy(x => x).ToArray();
        var rightColumn = data.Select(d => d[1]).Select(int.Parse).OrderBy(x => x).ToArray();

        var result = leftColumn.Zip(rightColumn, (l, r) => Math.Abs(l - r)).Sum();

        return result;
    }

    public override double SolvePuzzle2()
    {
        var data = LoadDataPerLineFromDay(1).Select(l => l.Split("   ")).ToList();

        var leftColumn = data.Select(d => d[0]).Select(int.Parse).OrderBy(x => x).ToArray();
        var rightColumn = data.Select(d => d[1]).Select(int.Parse).OrderBy(x => x).ToArray();

        var similarity = leftColumn.Select(d => d * rightColumn.Count(r => r == d)).Sum();

        return similarity;
    }
}