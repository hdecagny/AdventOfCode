namespace AdventOfCode2024;

public class Day4Solver : SolverBase2024
{
    private string[] _data;
    private int _length;
    private int _height;

    public override double SolvePuzzle1()
    {
        _data = LoadDataPerLineFromDay(4);
        _length = _data[0].Length;
        _height = _data.Length;

        var answer = 0;

        for (var x = 0; x < _length; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                answer += NumberOfWordsFound(x, y);
            }
        }

        return answer;
    }

    private int NumberOfWordsFound(int x, int y)
    {
        if (_data[y][x] != 'X')
        {
            return 0;
        }

        var movingLeft = (-1, 0);
        var movingRight = (1, 0);
        var movingUp = (0, -1);
        var movingDown = (0, 1);
        var movingLeftUp = (-1, -1);
        var movingRightUp = (1, -1);
        var movingLeftDown = (1, 1);
        var movingRightDown = (-1, 1);

        var answer = new[] { movingLeft, movingRight, movingUp, movingDown, movingLeftDown, movingRightDown, movingLeftUp, movingRightUp }
            .Sum(c => CheckWord(x, y, c));


        return answer;
    }

    private int CheckWord(int x, int y, (int Xdir, int Ydir) vector)
    {
        try
        {
            if (_data[y + vector.Ydir][x + vector.Xdir] == 'M'
                && _data[y + vector.Ydir * 2][x + vector.Xdir * 2] == 'A'
                && _data[y + vector.Ydir * 3][x + vector.Xdir * 3] == 'S')
            {
                return 1;
            }
        }
        catch
        {
            return 0;
        }

        return 0;
    }

    public override double SolvePuzzle2()
    {
        _data = LoadDataPerLineFromDay(4);
        _length = _data[0].Length;
        _height = _data.Length;

        var answer = 0;

        for (var x = 0; x < _length; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                answer += isXmas(x, y) ? 1 : 0;
            }
        }

        return answer;
    }

    private bool isXmas(int x, int y)
    {
        if (_data[y][x] != 'A')
        {
            return false;
        }

        try
        {
            var diag1 = new[] { _data[y + 1][x + 1], _data[y - 1][x - 1] };
            var diag2 = new[] { _data[y + 1][x - 1], _data[y - 1][x + 1] };

            var intersection = new[] { 'M', 'S' };

            var isxMas = (diag1.Intersect(intersection).Count() == 2 && diag2.Intersect(intersection).Count() == 2);

            return isxMas;
        }
        catch
        {
            return false;
        }
    }
}