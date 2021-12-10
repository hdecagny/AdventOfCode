using AdventOfCode.Data;

namespace AdventOfCode.Day10;
using MathNet.Numerics.Statistics;

public static class Day10Solver
{
    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(10)
            .ToList();

        return data.Sum(GetScorePart1);
    }

    private static int GetScorePart1(string input)
    {
        var stack = new Stack<char>();

        var openbracketList = new List<char> { '{', '[', '(', '<' };

        foreach (var bracket in input)
        {
            if (openbracketList.Contains(bracket))
            {
                stack.Push(bracket);
            }
            else
            {
                switch (bracket)
                {
                    case '}':
                    {
                        if (stack.Pop() != '{')
                        {
                            return 1197;
                        }

                        break;
                    }
                    case ')':
                    {
                        if (stack.Pop() != '(')
                        {
                            return 3;
                        }

                        break;
                    }
                    case ']':
                    {
                        if (stack.Pop() != '[')
                        {
                            return 57;
                        }

                        break;
                    }
                    case '>':
                    {
                        if (stack.Pop() != '<')
                        {
                            return 25137;
                        }

                        break;
                    }
                }
            }
        }

        return 0;
    }

    public static decimal SolvePuzzle2()
    {
        var data = DataLoader.LoadDataPerLineFromDay(10)
            .ToList();

        var test = data.Select(GetScorePart2).ToList();
        test = test.Where(s => s != 0).OrderBy(s=>s).ToList();

        var count = test.Count;

        return test[count/2];

    }

    private static decimal GetScorePart2(string input)
    {
        var stack = new Stack<char>();

        var openbracketList = new List<char> { '{', '[', '(', '<' };

        foreach (var bracket in input)
        {
            if (openbracketList.Contains(bracket))
            {
                stack.Push(bracket);
            }
            else
            {
                switch (bracket)
                {
                    case '}':
                    {
                        if (stack.Pop() != '{')
                        {
                            return 0;
                        }

                        break;
                    }
                    case ')':
                    {
                        if (stack.Pop() != '(')
                        {
                            return 0;
                        }

                        break;
                    }
                    case ']':
                    {
                        if (stack.Pop() != '[')
                        {
                            return 0;
                        }

                        break;
                    }
                    case '>':
                    {
                        if (stack.Pop() != '<')
                        {
                            return 0;
                        }

                        break;
                    }
                }
            }
        }

        decimal answer = 0;

        while (stack.TryPop(out var remainingchar))
        {
            answer = answer * 5 + _characterValue[remainingchar];
        }

        return answer;
    }

    private static readonly Dictionary<char, int> _characterValue = new Dictionary<char, int>()
    {
        { '(', 1 },
        { '[', 2 },
        { '{', 3 },
        { '<', 4 }
    };
}