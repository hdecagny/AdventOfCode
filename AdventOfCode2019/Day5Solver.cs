namespace AdventOfCode2019;

public class Day5Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var program = LoadDataFromDay(5).Split(',').Select(int.Parse).ToList();
        var position = 0;
        var result = -9999;

        while (position < program.Count)
        {
            var opcode = program[position];
            var instructions = IntCodeComputerStatic.ParseOpCode(opcode);
            switch (instructions.operation)
            {
                case 1:
                    program = IntCodeComputerStatic.PerformAddition(program,
                        position,
                        instructions.parameterModes);
                    position += 4;
                    break;
                case 2:
                    program = IntCodeComputerStatic.PerformMultiplication(program,
                        position,
                        instructions.parameterModes);
                    position += 4;
                    break;
                case 3:
                    program = IntCodeComputerStatic.SetInput(program, 1, program[position + 1]);
                    position += 2;
                    break;
                case 4:
                    result = IntCodeComputerStatic.GetOutput(program, program[position + 1]);
                    position += 2;
                    break;
                case 99:
                    return result;
            }
        }

        return -99;
    }

    public override double SolvePuzzle2()
    {
        var program = LoadDataFromDay(5).Split(',').Select(int.Parse).ToList();
        return IntCodeComputerStatic.RunIntCodeComputer(program, [5]);
    }
}