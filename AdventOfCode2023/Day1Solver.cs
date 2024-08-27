namespace AdventOfCode2023;

using System.Text.RegularExpressions;

public class Day1Solver : SolverBase2023
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataPerLineFromDay(1);

        var answer = data.Select(l => l.Where(char.IsDigit)).ToList()
                         .Select(l => l.First().ToString() + l.Last().ToString())
                         .Select(int.Parse)
                         .Sum();

        return answer;
    }

    public override double  SolvePuzzle2()
    {
        var data = LoadDataPerLineFromDay(1);

        var answer = data
                    .Select(l => Regex.Replace(l, "(oneight)", "18"))
                    .Select(l => Regex.Replace(l, "(threeight)", "38"))
                    .Select(l => Regex.Replace(l, "(fiveight)", "58"))
                    .Select(l => Regex.Replace(l, "(nineight)", "98"))
                    .Select(l => Regex.Replace(l, "(eightwo)", "82"))
                    .Select(l => Regex.Replace(l, "(eighthree)", "83"))
                    .Select(l => Regex.Replace(l, "(twone)", "21"))
                    .Select(l => Regex.Replace(l, "(one)", "1"))
                    .Select(l => Regex.Replace(l, "(two)", "2"))
                    .Select(l => Regex.Replace(l, "(three)", "3"))
                    .Select(l => Regex.Replace(l, "(four)", "4"))
                    .Select(l => Regex.Replace(l, "(five)", "5"))
                    .Select(l => Regex.Replace(l, "(six)", "6"))
                    .Select(l => Regex.Replace(l, "(seven)", "7"))
                    .Select(l => Regex.Replace(l, "(eight)", "8"))
                    .Select(l => Regex.Replace(l, "(nine)", "9"));

        var test = answer.Select(l => l.Where(char.IsDigit)).ToList()
                         .Select(l => l.First().ToString() + l.Last().ToString())
                         .Select(int.Parse)
                         .Sum();

        return test;
    }
}