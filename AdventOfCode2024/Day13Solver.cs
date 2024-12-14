namespace AdventOfCode2024;

using System.Text.RegularExpressions;

public class Day13Solver : SolverBase2024
{
    public override double SolvePuzzle1()
    {
        var input = LoadDataFromDay(13);

        var machines = input.Split("\r\n\r\n").Select(l => Regex.Matches(l, @"\d+").Select(m => int.Parse(m.ToString())).ToList())
            .ToList();

        return machines.Sum(SolveMachine);
    }

    private long SolveMachine(IReadOnlyList<int> machine)
    {
        (int X, int Y) Abutton = (machine[0], machine[1]);
        (int X, int Y) Bbutton = (machine[2], machine[3]);
        (int X, int Y) prize = (machine[4], machine[5]);

        var winningCombinations = Enumerable.Range(0, 100)
            .SelectMany(i => Enumerable.Range(0, 100).Select(j => (i, j)))
            .Where(c => Abutton.X * c.i + Bbutton.X * c.j == prize.X
                && Abutton.Y * c.i + Bbutton.Y * c.j == prize.Y)
            .ToList();

        var result = winningCombinations.Count != 0 ? winningCombinations.Min(c => c.i * 3 + c.j) : 0;

        return result;
    }

    public override double SolvePuzzle2()
    {
        var input = LoadDataFromDay(13);

        var machines = input.Split("\r\n\r\n").Select(l => Regex.Matches(l, @"\d+").Select(m => long.Parse(m.ToString())).ToList())
            .ToList();

        return machines.Sum(SolveMachine2);
    }

    private long SolveMachine2(IReadOnlyList<long> machine)
    {
        (long a, long d, long b, long e, long c, long f) = 
            (machine[0], machine[1], machine[2], machine[3], machine[4] + 10000000000000, machine[5] + 10000000000000);
        
        try
        {
            var j = (d * c - a * f) / (d * b - a * e);
            var i = (c - b * j) / a;

            if ((d * c - a * f) % (d * b - a * e) !=0 || (c - b * j) %a !=0)
            {
                return 0;
            }
            
            return i * 3 + j;
        }
        catch
        {
            return 0;
        }
    }
}