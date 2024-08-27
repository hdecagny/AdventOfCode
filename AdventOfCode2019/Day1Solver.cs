namespace AdventOfCode2019;

public class Day1Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataPerLineFromDay(1);

        var result = data.Select(int.Parse)
                         .Select(m => m / 3 - 2)
                         .Sum();

        return result;
    }

    public override double SolvePuzzle2()
    {
        var data = LoadDataPerLineFromDay(1);

        var result = data.Select(int.Parse)
                         .Select(GetTotalFuelNecessary)
                         .Sum();

        return result;
    }

    private int GetTotalFuelNecessary(int originalmass)
    {
        var answer = 0;
        var massToCalculate = originalmass;

        while (massToCalculate > 0)
        {
            var fuelToAdd = massToCalculate / 3 - 2;
            answer += Math.Max(fuelToAdd,0);
            massToCalculate = fuelToAdd;
        }

        return answer;
    }
}