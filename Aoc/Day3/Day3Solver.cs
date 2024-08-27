

namespace AdventOfCode.Day2;

public static class Day3Solver
{
    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(3)
            .ToList();

        var numberOfLines = data.Count;
        var length = data[0].Length;

        var gammabit = "";
        var epsilonbit = "";

        for (int i = 0; i < data[0].Length; i++)
        {
            var sum = data.Sum(s => int.Parse(s[i].ToString()));
            gammabit += sum > numberOfLines / 2 ? 1 : 0;
            epsilonbit += sum > numberOfLines / 2 ? 0 : 1;
        }

        return Convert.ToInt32(gammabit, 2) * Convert.ToInt32(epsilonbit, 2);
    }

    public static long SolvePuzzle2()
    {
        var data = DataLoader.LoadDataPerLineFromDay(3)
            .ToList();


        var oxygenRateList = DataLoader.LoadDataPerLineFromDay(3).ToList();
        var Co2RateList = DataLoader.LoadDataPerLineFromDay(3).ToList();

        for (int i = 0; i < data[0].Length; i++)
        {
            var oxygenRateListBit = oxygenRateList.Sum(s => int.Parse(s[i].ToString()));
            var mostCommonBit = oxygenRateListBit * 2 >= oxygenRateList.Count ? 1 : 0;
            oxygenRateList = oxygenRateList.Where(s => s[i].ToString() == mostCommonBit.ToString()).ToList();
            if (oxygenRateList.Count == 1)
            {
                break;
            }
        }

        for (int i = 0; i < data[0].Length; i++)
        {
            var Co2RateListBit = Co2RateList.Sum(s => int.Parse(s[i].ToString()));
            var leastCommonBit = Co2RateListBit * 2 >= Co2RateList.Count ? 0 : 1;
            Co2RateList = Co2RateList.Where(s => s[i].ToString() == leastCommonBit.ToString()).ToList();
            if (Co2RateList.Count == 1)
            {
                break;
            }
        }

        return Convert.ToInt32(oxygenRateList.Single(), 2) * Convert.ToInt32(Co2RateList.Single(), 2);
    }
}