namespace AdventOfCode2019;

public class Day2Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataFromDay(2).Split(',').Select(int.Parse).ToList();
        data[1] = 12;
        data[2] = 2;

        return Calculate(data);
    }

    public override double SolvePuzzle2()
    {
        for (var noun = 0; noun<100; noun++)
        {
            for (var verb = 0; verb < 100; verb++)
            {
                var data = LoadDataFromDay(2).Split(',').Select(int.Parse).ToList();
                data[1] = noun;
                data[2] = verb;
                var result = Calculate(data);
                if (result == 19690720)
                {
                    return 100 * noun + verb;
                }
            }
        }
        
        return -99;
    }

    private static int Calculate(List<int> data)
    {
        var pointerPosition = 0;

        while (data[pointerPosition] != 99)
        {
            switch (data[pointerPosition])
            {
                case 1:
                    data[data[pointerPosition + 3]] = data[data[pointerPosition + 1]] + data[data[pointerPosition + 2]];
                    pointerPosition += 4;
                    break;
                case 2:
                    data[data[pointerPosition + 3]] = data[data[pointerPosition + 1]] * data[data[pointerPosition + 2]];
                    pointerPosition += 4;
                    break;
            }
        }
        
        return data[0];
    }
}