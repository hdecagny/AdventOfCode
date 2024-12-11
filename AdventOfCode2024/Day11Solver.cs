namespace AdventOfCode2024;

public class Day11Solver : SolverBase2024
{
    private Dictionary<(long stone, int numberofBlinks), long> _numberOfStonesGeneratedInABlink = new();

    public override double SolvePuzzle1()
    {
        var input = LoadDataFromDay(11).Split(' ').Select(long.Parse).ToList();

        for (var i = 0; i < 25; i++)
        {
            input = Blink(input);
        }

        return input.Count;
    }

    private List<long> Blink(List<long> stones)
    {
        var answer = new List<long>();

        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                answer.Add(1);
            }

            else if (stone.ToString().Length % 2 == 0)
            {
                var stoneAsString = stone.ToString();
                var length = stoneAsString.Length;

                var firstStone = stoneAsString.Substring(0, length / 2);
                var secondStone = stoneAsString.Substring(length / 2, length / 2);

                answer.AddRange([long.Parse(firstStone), long.Parse(secondStone)]);
            }

            else
            {
                answer.Add(stone * 2024);
            }
        }

        return answer;
    }

    public override double SolvePuzzle2()
    {
        var input = LoadDataFromDay(11).Split(' ').Select(long.Parse).ToList();
        
        return input.Sum(s=> GetNumberOfStonesGeneratedInABlink(s,75));
    }

    private long GetNumberOfStonesGeneratedInABlink(long stone, int numberOfBlinks)
    {
        if (numberOfBlinks == 0)
        {
            return 1;
        }

        if (_numberOfStonesGeneratedInABlink.ContainsKey((stone, numberOfBlinks)))
        {
            return _numberOfStonesGeneratedInABlink[(stone, numberOfBlinks)];
        }

        var stonesGenerated = new List<long>();


        if (stone == 0)
        {
            stonesGenerated.Add(1);
        }

        else if (stone.ToString().Length % 2 == 0)
        {
            var stoneAsString = stone.ToString();
            var length = stoneAsString.Length;

            var firstStone = stoneAsString.Substring(0, length / 2);
            var secondStone = stoneAsString.Substring(length / 2, length / 2);

            stonesGenerated.AddRange([long.Parse(firstStone), long.Parse(secondStone)]);
        }

        else
        {
            stonesGenerated.Add(stone * 2024);
        }
        
        var answer = stonesGenerated.Select(s=> GetNumberOfStonesGeneratedInABlink(s, numberOfBlinks-1)).Sum();
        _numberOfStonesGeneratedInABlink.Add((stone, numberOfBlinks), answer);
        
        return answer;
    }
}