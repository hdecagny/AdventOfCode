namespace AdventOfCode2022.Day10;

public  class Day10Solver: SolverBase2022
{
    public  long SolvePuzzle1()
    {
        var input = LoadDataPerLineFromDay(10);

        var signalStrengthPerCycle = new Dictionary<int, long> { { 0, 1 } };
        var actualcycle = 1;

        foreach (var command in input)
        {
            var parsedCommand = command.Split(" ");
            if (parsedCommand[0] == "addx")
            {
                signalStrengthPerCycle.Add(actualcycle, signalStrengthPerCycle[actualcycle - 1]);
                actualcycle++;

                var valueToAdd = int.Parse(parsedCommand[1]);

                signalStrengthPerCycle.Add(actualcycle, signalStrengthPerCycle[actualcycle - 1] + valueToAdd);
                actualcycle++;
            }
            else
            {
                signalStrengthPerCycle.Add(actualcycle, signalStrengthPerCycle[actualcycle - 1]);
                actualcycle++;
            }
        }

        long answer = 0;

        for (var i = 20; i <= 180; i += 40)
        {
            answer = answer + i * signalStrengthPerCycle[i];
        }

        var answer2 = 20 * signalStrengthPerCycle[20] +
                      60 * signalStrengthPerCycle[60] +
                      100 * signalStrengthPerCycle[100] +
                      140 * signalStrengthPerCycle[140] +
                      180 * signalStrengthPerCycle[180] +
                      220 * signalStrengthPerCycle[220];

        return answer2;
    }


    public  double SolvePuzzle2()
    {
        var input = LoadDataPerLineFromDay(10);

        var signalStrengthPerCycle = new Dictionary<int, long> { { 0, 1 } };
        var isPixelLitPerCycle = new Dictionary<int, string>();
        var actualcycle = 1;

        foreach (var command in input)
        {
            var parsedCommand = command.Split(" ");
            if (parsedCommand[0] == "addx")
            {
                signalStrengthPerCycle.Add(actualcycle, signalStrengthPerCycle[actualcycle - 1]);
                isPixelLitPerCycle.Add(actualcycle, IsPixelLitPerCycle(actualcycle, (int) signalStrengthPerCycle[actualcycle]));
                actualcycle++;

                var valueToAdd = int.Parse(parsedCommand[1]);

                signalStrengthPerCycle.Add(actualcycle, signalStrengthPerCycle[actualcycle - 1] + valueToAdd);                
                isPixelLitPerCycle.Add(actualcycle, IsPixelLitPerCycle(actualcycle, (int) signalStrengthPerCycle[actualcycle]));
                actualcycle++;
            }
            else
            {
                signalStrengthPerCycle.Add(actualcycle, signalStrengthPerCycle[actualcycle - 1]);
                isPixelLitPerCycle.Add(actualcycle, IsPixelLitPerCycle(actualcycle, (int) signalStrengthPerCycle[actualcycle]));
                actualcycle++;
            }
        }
        
        Console.Write("#");

        for (var i = 1; i<=240; i++)
        {
            Console.Write(isPixelLitPerCycle[i]);
            if (i % 40 == 39)
            {
                Console.Write("\n");
            }
        }

        return 0;
    }

    private  string IsPixelLitPerCycle(int actualCycle, int spritePosition)
    {
        var spritePositions = new[] { spritePosition - 1, spritePosition, spritePosition + 1 };

        return spritePositions.Contains(actualCycle%40) ? "#" : ".";
    }
}