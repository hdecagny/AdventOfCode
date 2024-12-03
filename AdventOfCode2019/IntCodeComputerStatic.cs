namespace AdventOfCode2019;

public class IntCodeComputer
{
    private List<int> _program;
    private readonly List<int> _instructionList;
    private int _position;
    private int _instructionListPosition;
    public int Result { get; private set; }
    public bool IsCompleted { get; private set; }

    public IntCodeComputer(List<int> program)
    {
        _program = program;
        _instructionListPosition = 0;
        Result = -9999;
        IsCompleted = false;
        _instructionList = new List<int>();
    }

    public bool RunIntCodeComputer(int input)
    {
        _instructionList.Add(input);

        while (_position < _program.Count)
        {
            var opcode = _program[_position];
            var instructions = ParseOpCode(opcode);
            switch (instructions.operation)
            {
                case 1:
                    _program = PerformAddition(_program,
                        _position,
                        instructions.parameterModes);
                    _position += 4;
                    break;
                case 2:
                    _program = PerformMultiplication(_program,
                        _position,
                        instructions.parameterModes);
                    _position += 4;
                    break;
                case 3:
                    if (_instructionList.Count == _instructionListPosition) // means that we are waiting for instruction
                    {
                        return false;
                    }

                    _program = SetInput(_program, _instructionList[_instructionListPosition], _program[_position + 1]);
                    _position += 2;
                    _instructionListPosition += 1;
                    break;
                case 4:
                    Result = GetOutput(_program, _program[_position + 1]);
                    _position += 2;
                    break;
                case 5:
                    _position = PerformJumpIfTrue(_program,
                        _position,
                        instructions.parameterModes);
                    break;
                case 6:
                    _position = PerformJumpIfFalse(_program,
                        _position,
                        instructions.parameterModes);
                    break;
                case 7:
                    _program = PerformLessThan(_program, _position, instructions.parameterModes);
                    _position += 4;
                    break;
                case 8:
                    _program = PerformEquals(_program, _position, instructions.parameterModes);
                    _position += 4;
                    break;
                case 99:
                    IsCompleted = true;
                    return true;
            }
        }

        return false;
    }

    private static (int operation, IReadOnlyList<ParameterMode> parameterModes) ParseOpCode(int opCode)
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

    private static List<int> PerformAddition(List<int> program,
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

    private static List<int> PerformMultiplication(List<int> program,
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

    private static List<int> SetInput(List<int> program, int input, int position)
    {
        program[position] = input;
        return program;
    }

    private static int GetOutput(List<int> program, int position)
    {
        return program[position];
    }

    private static int PerformJumpIfTrue(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        return value1 != 0 ? value2 : position + 3;
    }

    private static int PerformJumpIfFalse(List<int> program,
        int position,
        IReadOnlyList<ParameterMode> parameters)
    {
        var parameter1 = program[position + 1];
        var parameter2 = program[position + 2];

        var value1 = parameters[0] == ParameterMode.PositionMode ? program[parameter1] : parameter1;
        var value2 = parameters[1] == ParameterMode.PositionMode ? program[parameter2] : parameter2;

        return value1 == 0 ? value2 : position + 3;
    }

    private static List<int> PerformLessThan(List<int> program,
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

    private static List<int> PerformEquals(List<int> program,
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