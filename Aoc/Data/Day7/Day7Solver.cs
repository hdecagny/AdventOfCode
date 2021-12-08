using System.Text.RegularExpressions;

namespace AdventOfCode.Data.Day7;

public static class Day7Solver
{
    public static long SolvePuzzle1()
    {
        var data = Regex
            .Split(DataLoader.LoadDataPerLineFromDay(7).Single(), @"\D+")
            .Select(int.Parse)
            .ToList();

        var max = data.Max();

        var ans = 999999999999;

        for (var i = 0; i < max; i++)
        {
            var sum = data.Sum(s => Math.Abs(s - i));
            if (sum > ans)
            {
                return ans;
            }
            ans = sum;
        }

        return 10;
    }

    public static double SolvePuzzle2()
    {
        var data = Regex
            .Split(DataLoader.LoadDataPerLineFromDay(7).Single(), @"\D+")
            .Select(int.Parse)
            .ToList();

        var max = data.Max();

        var ans = 999999999999999999;

        for (var i = 0; i < max; i++)
        {
            var sum = data.Sum(s => Math.Abs(s - i)*(Math.Abs(s - i)+1)/2);

            if (sum < ans)
            {
                ans = sum;
            }
        }

        return ans;
    }
}