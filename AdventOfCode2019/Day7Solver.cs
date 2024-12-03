namespace AdventOfCode2019;

public class Day7Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var program = LoadDataFromDay(7).Split(',').Select(int.Parse).ToList();

        var result = 0;

        var permutations = GetPermutations(Enumerable.Range(0, 5), 5);

        foreach (var possibility in permutations.ToList())
        {
            var possibililist = possibility.ToList();

            var computers = new[]
            {
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
            };

            var output = 0;

            for (var i = 0; i < 5; i++)
            {
                computers[i].RunIntCodeComputer(possibililist[i]);
                computers[i].RunIntCodeComputer(output);
                output = computers[i].Result;
            }

            result = Math.Max(output, result);
        }

        return result;
    }

    public override double SolvePuzzle2()
    {
        var program = LoadDataFromDay(7).Split(',').Select(int.Parse).ToList();

        var result = 0;

        var permutations = GetPermutations(Enumerable.Range(5, 5), 5);

        foreach (var possibility in permutations.ToList())
        {
            var possibililist = possibility.ToList();

            var computers = new[]
            {
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
                new IntCodeComputer(program.ToList()),
            };

            var output = 0;

            for (var i = 0; i < 5; i++) //initialization
            {
                computers[i].RunIntCodeComputer(possibililist[i]);
            }

            while (!computers[4].IsCompleted)
            {
                for (var i = 0; i < 5; i++)
                {
                    computers[i].RunIntCodeComputer(output);
                    output = computers[i].Result;
                }
            }

            result = Math.Max(output, result);
        }

        return result;
    }

    static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}