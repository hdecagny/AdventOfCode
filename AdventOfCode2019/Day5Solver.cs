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
            var instructions = IntCodeComputer.ParseOpCode(opcode);
            switch (instructions.operation)
            {
                case 1:
                    program = IntCodeComputer.PerformAddition(program,
                                                              program[position + 1],
                                                              program[position + 2],
                                                              program[position + 3],
                                                              instructions.parameterModes[0],
                                                              instructions.parameterModes[1]);
                    position += 4;
                    break;
                case 2:
                    program = IntCodeComputer.PerformMultiplication(program,
                                                                    program[position + 1],
                                                                    program[position + 2],
                                                                    program[position + 3],
                                                                    instructions.parameterModes[0],
                                                                    instructions.parameterModes[1]);
                    position += 4;
                    break;
                case 3:
                    program = IntCodeComputer.SetInput(program, 1, program[position + 1]);
                    position += 2;
                    break;
                case 4:
                    result = IntCodeComputer.GetOutput(program, program[position + 1]);
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
        var position = 0;
        var result = -9999;

        while (position < program.Count)
        {
            var opcode = program[position];
            var instructions = IntCodeComputer.ParseOpCode(opcode);
            switch (instructions.operation)
            {
                case 1:
                    program = IntCodeComputer.PerformAddition(program,
                                                              program[position + 1],
                                                              program[position + 2],
                                                              program[position + 3],
                                                              instructions.parameterModes[0],
                                                              instructions.parameterModes[1]);
                    position += 4;
                    break;
                case 2:
                    program = IntCodeComputer.PerformMultiplication(program,
                                                                    program[position + 1],
                                                                    program[position + 2],
                                                                    program[position + 3],
                                                                    instructions.parameterModes[0],
                                                                    instructions.parameterModes[1]);
                    position += 4;
                    break;
                case 3:
                    program = IntCodeComputer.SetInput(program, 1, program[position + 1]);
                    position += 2;
                    break;
                case 4:
                    result = IntCodeComputer.GetOutput(program, program[position + 1]);
                    position += 2;
                    break;
                case 5:
                    position = IntCodeComputer.PerformJumpIfTrue(program,
                                                                 position,
                                                                 instructions.parameterModes[0],
                                                                 instructions.parameterModes[1]);
                    break;
                case 6:
                    position = IntCodeComputer.PerformJumpIfFalse(program,
                                                                  position,
                                                                  instructions.parameterModes[0],
                                                                  instructions.parameterModes[1]);
                    break;
                case 7:
                    program = IntCodeComputer.PerformLessThan(program, position, instructions.parameterModes);
                    break;
                case 8:
                    program = IntCodeComputer.PerformEquals(program, position, instructions.parameterModes);
                    break;
                case 99:
                    return result;
            }
        }

        return -99;
    }
}