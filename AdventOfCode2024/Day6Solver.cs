namespace AdventOfCode2024;

using System.Runtime.Intrinsics.X86;
using System.Xml;

public class Day6Solver : SolverBase2024
{
    private int _height;
    private int _width;
    private int _surface;
    private char[][] _initialgrid;
    private char[][] _originalyPopulatedGrid;

    private char[][] _grid;

    public override double SolvePuzzle1()
    {
        _grid = LoadDataPerLineFromDay(6).Select(l => l.ToCharArray()).ToArray();

        _height = _grid.Length;
        _width = _grid[0].Length;

        PopulateGuardPath(_grid);

        var answer = _grid.Sum(l => l.Count(c => c is 'X' or '^'));

        return answer;
    }

    private char[][] PopulateGuardPath(char[][] grid)
    {
        var initialHeight = Array.FindIndex(grid, l => l.Contains('^'));
        var initialWidth = Array.FindIndex(grid.First(l => l.Contains('^')), l => l == '^');
        (int X, int Y) guardPosition = (initialWidth, initialHeight);
        (int Xdir, int Ydir) direction = (0, -1);

        while (!PositionOutsideTheGrid((guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir))) //Guard goes outside the grid 
        {
            // Move the guard
            grid[guardPosition.Y][guardPosition.X] = 'X';
            grid[guardPosition.Y + direction.Ydir][guardPosition.X + direction.Xdir] = '^';
            guardPosition = (guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir);

            if (PositionOutsideTheGrid((guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir)))
            {
                break;
            }

            //Possibly change guard direction
            while (NextPositionisBlocked((guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir)))
            {
                direction = (-direction.Ydir, direction.Xdir);
                if (PositionOutsideTheGrid((guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir)))
                {
                    break;
                }
            }
        }

        return grid;
    }

    private bool PositionOutsideTheGrid((int X, int Y) nextPosition)
    {
        return (nextPosition.Y == _height ||
            nextPosition.Y == -1 ||
            nextPosition.X == _width ||
            nextPosition.X == -1);
    }

    private bool NextPositionisBlocked((int X, int Y) nextPosition)
    {
        return _grid[nextPosition.Y][nextPosition.X] == '#';
    }

    public override double SolvePuzzle2()
    {
        var getGrid = () => LoadDataPerLineFromDay(6).Select(l => l.ToCharArray()).ToArray();

        _grid = LoadDataPerLineFromDay(6).Select(l => l.ToCharArray()).ToArray();

        _height = _grid.Length;
        _width = _grid[0].Length;
        _surface = _height * _width;
        _originalyPopulatedGrid = PopulateGuardPath(getGrid());

        var widthRange = Enumerable.Range(0, _width);
        var heightRange = Enumerable.Range(0, _width);

        return widthRange.Sum(x => heightRange.Count(y => isInfiniteLoop((x, y))));
    }

    private bool isInfiniteLoop((int X, int Y) singularityPosition)
    {
        if (singularityPosition.X == 6 && singularityPosition.Y == 3)
        {
            var test = 0;
        }
        
        if (_grid[singularityPosition.Y][singularityPosition.X] == '^') // the original guard never visited this place, adding a singluarity won't change anything
        {
            return false;
        }
        
        var grid = LoadDataPerLineFromDay(6).Select(l => l.ToCharArray()).ToArray();
        grid[singularityPosition.Y][singularityPosition.X] = '#'; 
        
        var initialHeight = Array.FindIndex(grid, l => l.Contains('^'));
        var initialWidth = Array.FindIndex(grid.First(l => l.Contains('^')), l => l == '^');
        (int X, int Y) guardPosition = (initialWidth, initialHeight);
        (int Xdir, int Ydir) direction = (0, -1);
        var footCount = 0;

        while (!PositionOutsideTheGrid((guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir))) //Guard goes outside the grid 
        {
            // Move the guard
            grid[guardPosition.Y][guardPosition.X] = 'X';
            grid[guardPosition.Y + direction.Ydir][guardPosition.X + direction.Xdir] = '^';
            guardPosition = (guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir);
            footCount += 1;

            if (footCount > _surface)
            {
                return true;
            }

            if (PositionOutsideTheGrid((guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir)))
            {
                break;
            }

            //Possibly change guard direction
            while (NextPositionisBlocked(grid, (guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir)))
            {
                direction = (-direction.Ydir, direction.Xdir);
                if (PositionOutsideTheGrid((guardPosition.X + direction.Xdir, guardPosition.Y + direction.Ydir)))
                {
                    break;
                }
            }
        }

        return false;
    }
    
    private bool NextPositionisBlocked(char[][] grid, (int X, int Y) nextPosition)
    {
        return grid[nextPosition.Y][nextPosition.X] == '#';
    }
}