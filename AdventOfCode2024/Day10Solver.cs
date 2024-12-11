namespace AdventOfCode2024;

using System.Numerics;

public class Day10Solver : SolverBase2024
{
    private Dictionary<Complex, int> _map;

    public override double SolvePuzzle1()
    {
        _map = LoadDataAsMapFromDay(10).ToDictionary(kvp => kvp.Key, kvp => int.Parse(kvp.Value.ToString()));

        var trailheads = _map.Where(kvp => kvp.Value == 0).ToList();

        return trailheads.Sum(CalculateTrailheadScore);
    }
    
    public override double SolvePuzzle2()
    {
        _map = LoadDataAsMapFromDay(10).ToDictionary(kvp => kvp.Key, kvp => int.Parse(kvp.Value.ToString()));

        var trailheads = _map.Where(kvp => kvp.Value == 0).ToList();

        return trailheads.Sum(CalculateTrailheadRatinScore);
    }

    private int CalculateTrailheadScore(KeyValuePair<Complex, int> currentPosition)
    {
        var currentheight = 0;
        var possiblePaths = new[] { currentPosition }.ToList();

        while (currentheight < 9)
        {
            possiblePaths = possiblePaths.SelectMany(GetPossibleNext).ToList();
            currentheight += 1;
        }

        return possiblePaths.Select(kvp=>kvp.Key).Distinct().Count();
    }
    
    private int CalculateTrailheadRatinScore(KeyValuePair<Complex, int> currentPosition)
    {
        var currentheight = 0;
        var possiblePaths = new[] { currentPosition }.ToList();

        while (currentheight < 9)
        {
            possiblePaths = possiblePaths.SelectMany(GetPossibleNext).ToList();
            currentheight += 1;
        }

        return possiblePaths.Count();
    }

    private IReadOnlyList<KeyValuePair<Complex, int>> GetPossibleNext(KeyValuePair<Complex, int> currentPosition)
    {
        return GetNeighbours(currentPosition.Key).Where(kvp => int.Parse(kvp.Value.ToString()) == currentPosition.Value + 1).ToList();
    }

    private IReadOnlyList<KeyValuePair<Complex, int>> GetNeighbours(Complex coordinate)
    {
        return _map.Where(k => new[]
            {
                coordinate + 1,
                coordinate - 1,
                coordinate + Complex.ImaginaryOne,
                coordinate - Complex.ImaginaryOne
            }.Contains(k.Key))
            .ToList();
    }
}