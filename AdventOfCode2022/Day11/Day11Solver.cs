
namespace AdventOfCode2022.Day11;

public  class Day11Solver : SolverBase2022
{
    public  long SolvePuzzle1()
    {
        var input = LoadDataFromDay(11);
        var monkeyStrings = input.Split("\r\n\r\n");

        var monkeys = monkeyStrings
            .Select(x => new Monkey(x))
            .OrderBy(x => x.MonkeyId)
            .ToList();

        for (var i = 1; i <= 20; i++)
        {
            DoAMonkeyRound(monkeys);
        }

        var monkeyPerMonkeyBusiness = monkeys.OrderByDescending(m => m.MonkeyBusiness).ToList();

        return monkeyPerMonkeyBusiness[0].MonkeyBusiness * monkeyPerMonkeyBusiness[1].MonkeyBusiness;
    }

    private  void DoAMonkeyRound(List<Monkey> monkeys)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.ListOfItems.TryDequeue(out var item))
            {
                var newWorrinessLevel = monkey.InspectionOperation(item) / 3;
                monkey.MonkeyBusiness += 1;
                monkeys.Single(m => m.MonkeyId == monkey.WorrinessTest(newWorrinessLevel))
                    .ListOfItems
                    .Enqueue(newWorrinessLevel);
            }
        }
    }


    public  long SolvePuzzle2()
    {
        var input = LoadDataFromDay(11);
        var monkeyStrings = input.Split("\r\n\r\n");

        var monkeys = monkeyStrings
            .Select(x => new Monkey(x))
            .OrderBy(x => x.MonkeyId)
            .ToList();

        for (var i = 1; i <= 10000; i++)
        {
            DoAMonkeyRound2(monkeys);
        }

        var monkeyPerMonkeyBusiness = monkeys.OrderByDescending(m => m.MonkeyBusiness).ToList();

        return monkeyPerMonkeyBusiness[0].MonkeyBusiness * monkeyPerMonkeyBusiness[1].MonkeyBusiness;
    }

    private  void DoAMonkeyRound2(List<Monkey> monkeys)
    {
        var commonDeviser = 1;
        
        foreach (var t in monkeys)
        {
            commonDeviser *= t.Diviser;
        }
        
        foreach (var monkey in monkeys)
        {
            while (monkey.ListOfItems.TryDequeue(out var item))
            {
                var newWorrinessLevel = (monkey.InspectionOperation(item)) % (commonDeviser);
                monkey.MonkeyBusiness += 1;
                monkeys.Single(m => m.MonkeyId == monkey.WorrinessTest(newWorrinessLevel))
                    .ListOfItems
                    .Enqueue(newWorrinessLevel);
            }
        }
    }
}