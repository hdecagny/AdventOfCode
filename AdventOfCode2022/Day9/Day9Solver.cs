using AdventOfCode.Data;

namespace AdventOfCode.Day9;

public static class Day9Solver
{
    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(9)
            .Select(s => s.ToCharArray().Select(c => int.Parse(c.ToString())).ToList())
            .ToList();

        var length = data[1].Count;
        var height = data.Count;

        var answer = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < length; x++)
            {
                answer += GetRiskLevel(data, x, y);
            }
        }

        return answer;
    }

    private static int GetRiskLevel(List<List<int>> lavamap, int x, int y)
    {
        var length = lavamap[1].Count;
        var height = lavamap.Count;

        var currentHeight = lavamap[y][x];

        if ((y == 0 || lavamap[y - 1][x] > currentHeight)
            && (y == height - 1 || lavamap[y + 1][x] > currentHeight)
            && (x == 0 || lavamap[y][x - 1] > currentHeight)
            && (x == length - 1 || lavamap[y][x + 1] > currentHeight))
        {
            return currentHeight + 1;
        }

        return 0;
    }

    public static double SolvePuzzle2()
    {
        var data = DataLoader.LoadDataPerLineFromDay(9)
            .Select(s => s.ToCharArray().Select(c => int.Parse(c.ToString())).ToList())
            .ToList();

        var length = data[1].Count;
        var height = data.Count;

        var lowpointsList = new List<(int, int)>();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < length; x++)
            {
                if (GetRiskLevel(data, x, y) != 0)
                {
                    lowpointsList.Add((x, y));
                }
            }
        }

        var bassinList = lowpointsList
            .Select(lowpoint => GetBassinSize(data, lowpoint.Item1, lowpoint.Item2))
            .ToList();

        var top3 = bassinList.OrderByDescending(s=>s).Take(3).ToList();

        return top3[0] * top3[1] * top3[2];
    }


    private static int GetBassinSize(List<List<int>> lavamap, int xLowestPoint, int yLowestPoint)
    {
        var length = lavamap[1].Count;
        var height = lavamap.Count;

        var pointsInBassin = new List<(int, int)>();
        var coordinatesToCheck = new Queue<(int, int)>();

        pointsInBassin.Add((xLowestPoint, yLowestPoint));
        coordinatesToCheck.Enqueue((xLowestPoint, yLowestPoint));

        while (coordinatesToCheck.TryDequeue(out var actualCoordinate))
        {
            var x = actualCoordinate.Item1;
            var y = actualCoordinate.Item2;
            var currentHeight = lavamap[y][x];

            if (y != 0
                && lavamap[y - 1][x] != 9
                && lavamap[y - 1][x] > currentHeight
                && !pointsInBassin.Contains((x, y - 1)))
            {
                pointsInBassin.Add((x, y - 1));
                coordinatesToCheck.Enqueue((x, y - 1));
            }

            if (y != height - 1
                && lavamap[y + 1][x] != 9
                && lavamap[y + 1][x] > currentHeight
                && !pointsInBassin.Contains((x, y + 1)))
            {
                pointsInBassin.Add((x, y + 1));
                coordinatesToCheck.Enqueue((x, y + 1));
            }

            if (x != 0
                && lavamap[y][x - 1] != 9
                && lavamap[y][x - 1] > currentHeight
                && !pointsInBassin.Contains((x - 1, y)))
            {
                pointsInBassin.Add((x - 1, y));
                coordinatesToCheck.Enqueue((x - 1, y));
            }

            if (x != length - 1
                && lavamap[y][x + 1] != 9
                && lavamap[y][x + 1] > currentHeight
                && !pointsInBassin.Contains((x + 1, y))
                )
            {
                pointsInBassin.Add((x + 1, y));
                coordinatesToCheck.Enqueue((x + 1, y));
            }
        }


        return pointsInBassin.Count;
    }
}