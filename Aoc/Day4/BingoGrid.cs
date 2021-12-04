namespace AdventOfCode.Day4
{
    public class BingoGrid
    {
        private int[] _grid { get; set; }

        private readonly int _gridLength = 5;

        public BingoGrid(int[] grid)
        {
            _grid = grid;
        }

        public void CheckNumber(int bingoNumber)
        {
            for(var i=0;i< _grid.Length;i++)
            {
                if (_grid[i] == bingoNumber)
                {
                    _grid[i] = 0;
                }
            }
        }

        public bool IsGridSolved()
        {
            return SolveDiagonal() || SolveHorizontalLines() || SolveVerticalLines();
        }
        public int GridScore()
        {
            return IsGridSolved() ? _grid.Sum() : 0; ;
        }

        private bool SolveDiagonal()
        {
            var line = _grid.Where((x, i) => i % (_gridLength+1) == 0);

            return line.Sum() == 0;
        }

        private bool SolveHorizontalLines()
        {
            for(var i=0;i< _gridLength; i++)
            {
                var line = _grid.Where((x, index) => index % _gridLength == i);
                if (line.Sum() == 0) { return true; }
            }

            return false;
        }

        private bool SolveVerticalLines()
        {
            for (var i = 0; i < _gridLength; i++)
            {
                var line = _grid.Skip(i* _gridLength).Take(5);
                if (line.Sum() == 0) { return true; }
            }

            return false;
        }
    }
}
