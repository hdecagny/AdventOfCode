namespace AdventOfCode2024;

public class Day5Solver : SolverBase2024
{
    public override double SolvePuzzle1()
    {
        var input = LoadDataFromDay(5).Split("\r\n\r\n");


        var rules = input[0].Split("\r\n").Select(l => l.Split('|').Select(int.Parse).ToList()).ToList();
        var updates = input[1].Split("\r\n").Select(l => l.Split(',').Select(int.Parse).ToList()).ToList();

        var answer = updates.Where(l => IsInRightOrder(l, rules))
            .Sum(l => l[(l.Count - 1) / 2]);

        return answer;
    }

    private bool IsInRightOrder(List<int> update, List<List<int>> rules)
    {
        var relevantRules = rules.Where(m => update.Contains(m[0]) && update.Contains(m[1])).ToList();

        var result = relevantRules.All(relevantRule =>
            update.FindIndex(u => u == relevantRule[0]) <= update.FindIndex(u => u == relevantRule[1]));

        return result;
    }

    public override double SolvePuzzle2()
    {
        var input = LoadDataFromDay(5).Split("\r\n\r\n");

        var rules = input[0].Split("\r\n").Select(l => l.Split('|').Select(int.Parse).ToList()).ToList();
        var updates = input[1].Split("\r\n").Select(l => l.Split(',').Select(int.Parse).ToList()).ToList();

        var result = updates.Where(l => !IsInRightOrder(l, rules))
            .Select(l => PutInRightOrder(l, rules.Select(r => (r[0], r[1])).ToList()))
            .Sum(l => l[(l.Count - 1) / 2]);

        return result;
    }

    private List<int> PutInRightOrder(List<int> update, List<(int x, int y)> rules)
    {
        var relevantRules = rules.Where(m => update.Contains(m.x) && update.Contains(m.y)).ToList();

        var comparer = Comparer<int>.Create((p1, p2) => relevantRules.Contains((p1, p2)) ? -1 : 1);

        var result = update.OrderBy(u => u, comparer).ToList();

        return result;
    }
}