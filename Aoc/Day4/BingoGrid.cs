namespace AdventOfCode.Day4
{
    public class BingoGrid
    {
        public int[] Grid { get; set; }

        private readonly int _gridLength = 5;

        public void CheckNumber(int bingoNumber)
        {
            foreach(var i in Grid)
            {
                if (i == bingoNumber)
                {
                    i == 0;
                }
            }
        }

        public bool IsGridSolved()
        {
            SolveDiagonal();
            SolveHorizontalLines();
            SolveVerticalLines();
        }

        private bool SolveDiagonal()
        {

        }

        private bool SolveHorizontalLines()
        {
            for(var i=0;i< _gridLength; i++)
            {
                var sum = Grid.Where((x, i) => i % _gridLength == i);
                if (sum.Sum() == 0) { return true; }
            }

            return false;
        }

        private bool SolveVerticalLines()
        {
            for (var i = 0; i < _gridLength; i++)
            {
                var sum = Grid.Skip().Take(5);
                if (sum.Sum() == 0) { return true; }
            }

            return false;
        }
    }
}
