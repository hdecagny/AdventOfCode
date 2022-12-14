using AdventOfCode.Data;

namespace AdventOfCode.Day9;

public static class Day9Solver
{
    public static long SolvePuzzle1()
    {
        var input = DataLoader.LoadDataPerLineFromDay(9);

        var headPosition = (0, 0);
        var tailPosition = (0, 0);
        var visitedPositions = new List<(int, int)>() { (0, 0) };

        foreach (var command in input)
        {
            var parsedCommand = command.Split(" ");
            var direction = parsedCommand[0];
            var iteration = int.Parse(parsedCommand[1]);

            for (var i = 1; i <= iteration; i++)
            {
                headPosition = DetermineNewHeadPosition(headPosition, direction);
                tailPosition = DetermineNewTailPosition(headPosition, tailPosition);

                if (!visitedPositions.Contains(tailPosition))
                {
                    visitedPositions.Add(tailPosition);
                }
            }
        }

        return visitedPositions.Count;
    }

    private static (int, int) DetermineNewHeadPosition((int, int) originalHeadPosition, string direction)
    {
        return direction switch
               {
                   "R" => (originalHeadPosition.Item1, originalHeadPosition.Item2 + 1),
                   "L" => (originalHeadPosition.Item1, originalHeadPosition.Item2 - 1),
                   "U" => (originalHeadPosition.Item1 + 1, originalHeadPosition.Item2),
                   "D" => (originalHeadPosition.Item1 - 1, originalHeadPosition.Item2),
                   _ => throw new NotImplementedException()
               };
    }

    private static (int, int) DetermineNewTailPosition((int, int) originalHeadPosition, (int, int) originalTailPosition)
    {
        if (ComputeSquareDistance(originalHeadPosition, originalTailPosition) <= 2)
        {
            return originalTailPosition;
        }

        var newX = originalTailPosition.Item1 + Math.Sign(originalHeadPosition.Item1 - originalTailPosition.Item1);
        var newY = originalTailPosition.Item2 + Math.Sign(originalHeadPosition.Item2 - originalTailPosition.Item2);

        return (newX, newY);
    }

    private static double ComputeSquareDistance((int, int) originalHeadPosition, (int, int) originalTailPosition)
    {
        return Math.Pow(originalHeadPosition.Item1 - originalTailPosition.Item1, 2) + Math.Pow(originalHeadPosition.Item2 - originalTailPosition.Item2, 2);
    }


    public static double SolvePuzzle2()
    {
        var input = DataLoader.LoadDataPerLineFromDay(9);

        var ropePosition = new List<(int, int)>()
        {
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0),
            (0, 0)
        };

        var visitedPositions = new List<(int, int)>() { (0, 0) };

        foreach (var command in input)
        {
            var parsedCommand = command.Split(" ");
            var direction = parsedCommand[0];
            var iteration = int.Parse(parsedCommand[1]);

            for (var i = 1; i <= iteration; i++)
            {
                //First move the head 
                ropePosition[0] = DetermineNewHeadPosition(ropePosition[0], direction);
                
                //Then move the 9 knots depending on the position of the previous node
                for (var j = 1; j <= 9; j++)
                {
                    var previousNode = ropePosition[j - 1];
                    var actualNode = ropePosition[j];
                    ropePosition[j] = DetermineNewTailPosition(previousNode, actualNode);
                }
                
                if (!visitedPositions.Contains(ropePosition[9]))
                {
                    visitedPositions.Add(ropePosition[9]);
                }
            }
        }

        return visitedPositions.Count;
    }
}