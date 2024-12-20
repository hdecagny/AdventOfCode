﻿namespace AdventOfCode2019;

public class IntCodeComputerStatic
{
    public static int RunIntCodeComputer(List<int> program, List<int> inputs)
    {
        var programPosition = 0;
        var inputPosition = 0;
        var result = -9999;

        while (programPosition < program.Count)
        {
            var opcode = program[programPosition];
            var instructions = ParseOpCode(opcode);
            switch (instructions.operation)
            {
                case 1:
                    program = PerformAddition(program,
                        programPosition,
                        instructions.parameterModes);
                    programPosition += 4;
                    break;
                case 2:
                    program = PerformMultiplication(program,
                        programPosition,
                        instructions.parameterModes);
                    programPosition += 4;
                    break;
                case 3:
                    program = SetInput(program, inputs[inputPosition], program[programPosition + 1]);
                    inputPosition += 1;
                    programPosition += 2;
                    break;
                case 4:
                    result = GetOutput(program, program[programPosition + 1]);
                    programPosition += 2;
                    break;
                case 5:
                    programPosition = PerformJumpIfTrue(program,
                        programPosition,
                        instructions.parameterModes);
                    break;
                case 6:
                    programPosition = PerformJumpIfFalse(program,
                        programPosition,
                        instructions.parameterModes);
                    break;
                case 7:
                    program = PerformLessThan(program, programPosition, instructions.parameterModes);
                    programPosition += 4;
                    break;
                case 8:
                    program = PerformEquals(program, programPosition, instructions.parameterModes);
                    programPosition += 4;
                    break;
                case 99:
                    return result;
            }
        }

        return result;
    }

    public static (int operation, IReadOnlyList<ParameterMode> parameterModes) ParseOpCode(int opCode)
    {
        if (opCode is >= 0 and < 100)
        {
            return (opCode, [ParameterMode.PositionMode, ParameterMode.PositionMode, ParameterMode.PositionMode, ParameterMode.PositionMode]);
        }

        var opCodeString = opCode.ToString();

        var operation = int.Parse(opCodeString.Substring(opCodeString.Length - 2));
        var parameterList = new List<ParameterMode>();

        for (var i = opCodeString.Length - 3; i >= 0; i--)
        {
            parameterList.Add((ParameterMode)int.Parse(opCodeString[i].ToString()));
        }

        parameterList.AddRange([
            ParameterMode.PositionMode,
            ParameterMode.PositionMode,
            ParameterMode.PositionMode,
            ParameterMode.PositionMode,
        ]);

        return (operation, parameterList);
    }

    public static List<int> PerformAddition(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];
        var parameter3 = program[position + 3];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        program[parameter3] = value1 + value2;

        return program;
    }

    public static List<int> PerformMultiplication(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];
        var parameter3 = program[position + 3];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        program[parameter3] = value1 * value2;

        return program;
    }

    public static List<int> SetInput(List<int> program, int input, int position)
    {
        program[position] = input;
        return program;
    }

    public static int GetOutput(List<int> program, int position)
    {
        return program[position];
    }

    public static int PerformJumpIfTrue(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        return value1 != 0 ? value2 : position + 3;
    }

    public static int PerformJumpIfFalse(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        return value1 == 0 ? value2 : position + 3;
    }

    public static List<int> PerformLessThan(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];
        var parameter3 = program[position + 3];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        program[parameter3] = value1 < value2 ? 1 : 0;

        return program;
    }

    public static List<int> PerformEquals(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];
        var parameter3 = program[position + 3];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        program[parameter3] = value1 == value2 ? 1 : 0;

        return program;
    }
}

public enum ParameterMode
{
    PositionMode = 0,
    ImmediateMode = 1
}