namespace AdventOfCode2019;

public class IntCodeComputer
{
    public static (int operation, IReadOnlyList<ParameterMode> parameterModes) ParseOpCode(int opCode)
    {
        if (opCode is >= 0 and < 100)
        {
            return (opCode, [ParameterMode.PositionMode, ParameterMode.PositionMode]);
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
                                            int parameter1,
                                            int parameter2,
                                            int locationResult,
                                            ParameterMode parameterMode1,
                                            ParameterMode parameterMode2)
    {
        var value1 = parameterMode1 == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameterMode2 == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        program[locationResult] = value1 + value2;

        return program;
    }

    public static List<int> PerformMultiplication(List<int> program,
                                                  int parameter1,
                                                  int parameter2,
                                                  int locationResult,
                                                  ParameterMode parameterMode1,
                                                  ParameterMode parameterMode2)
    {
        var value1 = parameterMode1 == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameterMode2 == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        program[locationResult] = value1 * value2;

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
                                        ParameterMode parameterMode1,
                                        ParameterMode parameterMode2)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];

        var value1 = parameterMode1 == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameterMode2 == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        return value1 != 0 ? value2 : position + 3;
    }

    public static int PerformJumpIfFalse(List<int> program,
                                         int position,
                                         ParameterMode parameterMode1,
                                         ParameterMode parameterMode2)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];

        var value1 = parameterMode1 == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameterMode2 == ParameterMode.PositionMode ? program[parameter2] : parameter2;

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
        var value3 = parameters[2] == ParameterMode.PositionMode ? program[parameter3] : parameter3;

        program[value3] = value1 < value2 ? 1 : 0;

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
        var value3 = parameters[2] == ParameterMode.PositionMode ? program[parameter3] : parameter3;

        program[value3] = value1 == value2 ? 1 : 0;

        return program;
    }
}

public enum ParameterMode
{
    PositionMode = 0,
    ImmediateMode = 1
}