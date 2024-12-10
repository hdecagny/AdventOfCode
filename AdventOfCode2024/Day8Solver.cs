namespace AdventOfCode2024;

using System.Numerics;

public class Day8Solver : SolverBase2024
{
    public override double SolvePuzzle1()
    {
        var map = LoadDataAsMapFromDay(8);

        var differentAntennasKind = map.Values.Where(c => c != '.').Distinct().ToList();

        var antennaslocationPerKind = differentAntennasKind.ToDictionary(c => c, c => map.Keys.Where(k => map[k] == c).ToList());

        var antinodesPositions = antennaslocationPerKind.Values
            .SelectMany(GetPossibleCouples)
            .SelectMany(c => GetAntinodePosition(c.a, c.b))
            .Distinct()
            .Where(v=> map.ContainsKey(v))
            .ToList();


        return antinodesPositions.Count;
    }

    private IReadOnlyList<(Complex a, Complex b)> GetPossibleCouples(List<Complex> differentAntennasPositions)
    {
        var size = differentAntennasPositions.Count;

        var answer = Enumerable.Range(0, size)
            .SelectMany(i => Enumerable.Range(0, size).Where(j => j < i)
                .Select(j => (differentAntennasPositions[i], differentAntennasPositions[j])))
            .ToList();

        return answer;
    }

    private List<Complex> GetAntinodePosition(Complex a, Complex b)
    {
        var distance = b - a;

        return [a - distance, b + distance];
    }
    
    private List<Complex> GetAntinodePositionWithHarmonies(Complex a, Complex b, int max)
    {
        var distance = b - a;

        var result = Enumerable.Range(0, max).Select(i => a - i * distance)
            .Concat(Enumerable.Range(0, max).Select(i => b + i * distance))
            .ToList();

        return result;
    }

    public override double SolvePuzzle2()
    {
        var map = LoadDataAsMapFromDay(8);

        var max = (int) map.Keys.Select(k => Math.Abs(k.Imaginary)).Max();
        var differentAntennasKind = map.Values.Where(c => c != '.').Distinct().ToList();
        var antennaslocationPerKind = differentAntennasKind.ToDictionary(c => c, c => map.Keys.Where(k => map[k] == c).ToList());

        var antinodesPositions = antennaslocationPerKind.Values
            .SelectMany(GetPossibleCouples)
            .SelectMany(c => GetAntinodePositionWithHarmonies(c.a, c.b, max))
            .Distinct()
            .Where(v=> map.ContainsKey(v))
            .ToList();


        return antinodesPositions.Count;
    }
}