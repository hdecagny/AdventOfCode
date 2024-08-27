namespace AdventOfCode2022.Day8;

public  class Day8Solver : SolverBase2022
{
    private  int[][] _input = new int[][] { };
    private  int _forestHeight;
    private  int _forestWidth;

    public  long SolvePuzzle1()
    {
        _input = LoadDataPerLineFromDay(8)
            .Select(l => l.ToArray()
                .Select(i => int.Parse(i.ToString()))
                .ToArray())
            .ToArray();

        return FindVisibleTrees();
    }

    private  long FindVisibleTrees()
    {
        var answer = 0;
        _forestHeight = _input.Length;
        _forestWidth = _input[0].Length;

        for (var i = 0; i < _forestHeight; i++)
        {
            for (var j = 0; j < _forestWidth; j++)
            {
                if (!IsOnTheEdge(i, j)
                    && !IsVisibleFromEast(i, j)
                    && !IsVisibleFromNorth(i, j)
                    && !IsVisibleFromSouth(i, j)
                    && !IsVisibleFromWest(i, j)) continue;
                answer++;
            }
        }

        return answer;
    }

    private  bool IsOnTheEdge(long heightCoordinate,
        long widthCoordinate)
    {
        return (heightCoordinate == 0
                || widthCoordinate == 0
                || heightCoordinate == _forestHeight - 1
                || widthCoordinate == _forestWidth - 1);
    }

    private  bool IsVisibleFromWest(long heightCoordinate,
        long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];

        for (var i = 0; i < widthCoordinate; i++)
        {
            if (_input[heightCoordinate][i] >= treeHeight)
            {
                return false;
            }
        }

        return true;
    }

    private  bool IsVisibleFromEast(long heightCoordinate,
        long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];

        for (var i = widthCoordinate + 1; i < _forestWidth; i++)
        {
            if (_input[heightCoordinate][i] >= treeHeight)
            {
                return false;
            }
        }

        return true;
    }

    private  bool IsVisibleFromNorth(long heightCoordinate, long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];

        for (var i = 0; i < heightCoordinate; i++)
        {
            if (_input[i][widthCoordinate] >= treeHeight)
            {
                return false;
            }
        }

        return true;
    }

    private  bool IsVisibleFromSouth(long heightCoordinate,
        long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];

        for (var i = heightCoordinate + 1; i < _forestHeight; i++)
        {
            if (_input[i][widthCoordinate] >= treeHeight)
            {
                return false;
            }
        }

        return true;
    }

    public  double SolvePuzzle2()
    {
        _input = LoadDataPerLineFromDay(8)
            .Select(l => l.ToArray()
                .Select(i => int.Parse(i.ToString()))
                .ToArray())
            .ToArray();
        
        _forestHeight = _input.Length;
        _forestWidth = _input[0].Length;

        return FindMaxScenicScore();
    }

    private  long FindMaxScenicScore()
    {
        long answer = 0;

        for (var i = 1; i < _forestHeight - 1; i++)
        {
            for (var j = 1; j < _forestWidth - 1; j++)
            {
                if (i == 3 && j == 2)
                {
                    var test = 0;
                }
                var scenicScore = CalculateWesternScenicScore(i, j) *
                                  CalculateEasternScenicScore(i, j) *
                                  CalculateNorthernScenicScore(i, j) *
                                  CalculateSouthernScenicScore(i, j);

                if (scenicScore > answer)
                {
                    answer = scenicScore;
                }
            }
        }

        return answer;
    }

    private  long CalculateWesternScenicScore(long heightCoordinate, long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];
        var answer = 1;

        for (var i = widthCoordinate-1; i >= 1; i -= 1)
        {
            if (_input[heightCoordinate][i] >= treeHeight)
            {
                return answer;
            }

            answer += 1;
        }

        return answer;
    }

    private  long CalculateEasternScenicScore(long heightCoordinate, long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];
        var answer = 1;

        for (var i = widthCoordinate+1; i <= _forestWidth-2; i++)
        {
            if (_input[heightCoordinate][i] >= treeHeight)
            {
                return answer;
            }

            answer += 1;
        }

        return answer;
    }

    private  long CalculateNorthernScenicScore(long heightCoordinate, long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];
        var answer = 1;

        for (var i = heightCoordinate-1; i >= 1; i -= 1)
        {
            if (_input[i][widthCoordinate] >= treeHeight)
            {
                return answer;
            }

            answer += 1;
        }

        return answer;
    }

    private  long CalculateSouthernScenicScore(long heightCoordinate, long widthCoordinate)
    {
        var treeHeight = _input[heightCoordinate][widthCoordinate];
        var answer = 1;

        for (var i = heightCoordinate+1; i < _forestHeight-2; i++)
        {
            if (_input[i][widthCoordinate] >= treeHeight)
            {
                return answer;
            }

            answer += 1;
        }

        return answer;
    }
}