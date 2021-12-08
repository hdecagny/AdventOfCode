using AdventOfCode.Data;
using AdventOfCode.Day4;

namespace AdventOfCode.Day2;

public class Day4Solver
{
    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataFromDay(4).Split("\r\n\r\n").ToList();

        var numbersToDraw = data[0].Split(",").Select(x => int.Parse(x)).ToList();

        var bingogrids = ConvertToBingoGrid(data.Skip(1).ToList());

        foreach (var number in numbersToDraw)
        {
            bingogrids.ForEach(b => b.CheckNumber(number));

            if (bingogrids.Any(b => b.IsGridSolved()))
            {
                return bingogrids.Max(b => b.GridScore() * number);
            }
        }

        return 0;
    }

    public static long SolvePuzzle2()
    {
        var data = DataLoader.LoadDataFromDay(4).Split("\r\n\r\n").ToList();

        var numbersToDraw = data[0].Split(",").Select(x => int.Parse(x)).ToList();

        var bingogrids = ConvertToBingoGrid(data.Skip(1).ToList());

        foreach (var number in numbersToDraw)
        {
            bingogrids.ForEach(b => b.CheckNumber(number));


            if (bingogrids.Count==1 && bingogrids.Single().IsGridSolved())
            {
                return bingogrids.Single().GridScore() * number;
            }

            bingogrids = bingogrids.Where(b => !b.IsGridSolved()).ToList();
        }

        return 0;
    }

    private static List<BingoGrid> ConvertToBingoGrid(IEnumerable<string> data)
    {
        return data
            .Skip(1)
            .Select(b => b.Replace("\r\n", " ").Replace("  ", " ")).ToList()
            .Select(b => b.Split(" ")).ToList()
            .Select(bi => bi.Where(s => s != "").Select(k => int.Parse(k)).ToArray())
            .Select(grid => new BingoGrid(grid))
            .ToList();
    }
}
