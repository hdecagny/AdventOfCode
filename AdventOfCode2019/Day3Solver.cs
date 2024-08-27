namespace AdventOfCode2019;

public class Day3Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var puzzle = LoadDataPerLineFromDay(3);

        var positionWire1 = GetWirePositions(puzzle[0]);
        var positionWire2 = GetWirePositions(puzzle[1]);

        var crossover = positionWire1.Intersect(positionWire2).Where(x=> x!=(0,0)).ToList();
        var manhattandistances = crossover.Select(p => Math.Abs(p.x) + Math.Abs(p.y));

        return manhattandistances.Min();
    }

    private List<(int x, int y)> GetWirePositions(string input)
    {
        var currentposition = (0, 0);
        var parsedInputs = input.Split(',');
        var positionlist = new List<(int x, int y)>();
        positionlist.Add(currentposition);

        foreach (var parsedinput in parsedInputs)
        {
            var direction = parsedinput[0];
            var length = int.Parse(parsedinput[1..]);

            for (var i = 0; i < length; i++)
            {
                switch (direction)
                {
                    case 'D':
                        currentposition = (currentposition.Item1, currentposition.Item2 - 1);
                        positionlist.Add(currentposition);
                        break;
                    case 'U':
                        currentposition = (currentposition.Item1, currentposition.Item2 + 1);
                        positionlist.Add(currentposition);
                        break;
                    case 'L':
                        currentposition = (currentposition.Item1 - 1, currentposition.Item2);
                        positionlist.Add(currentposition);
                        break;
                    case 'R':
                        currentposition = (currentposition.Item1 + 1, currentposition.Item2);
                        positionlist.Add(currentposition);
                        break;
                }
            }
        }

        return positionlist;
    }

    public override double SolvePuzzle2()
    {
        var puzzle = LoadDataPerLineFromDay(3);

        var positionWire1 = GetWirePositions(puzzle[0]);
        var positionWire2 = GetWirePositions(puzzle[1]);

        var crossovers = positionWire1.Intersect(positionWire2).Where(x=> x!=(0,0)).ToList();
        
        var answer = crossovers.Select(c => positionWire1.IndexOf(c) + positionWire2.IndexOf(c)).ToList();

        return answer.Min();
    }
}