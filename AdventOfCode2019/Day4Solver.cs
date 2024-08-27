namespace AdventOfCode2019;

public class Day4Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var possibleSolutions = Enumerable.Range(146810, 612564 - 146810).ToList();

        var answer = possibleSolutions.Where(CountainsADouble)
                                      .Where(DigitsNeverDecrease)
                                      .ToList();

        return answer.Count;
    }

    public override double SolvePuzzle2()
    {
        var possibleSolutions = Enumerable.Range(146888, 612564 - 146810).ToList();

        var answer = possibleSolutions.Where(CountainsADoubleWhichisNotPartOfABiggerDouble)
                                      .Where(DigitsNeverDecrease)
                                      .ToList();

        return answer.Count;
    }

    private bool CountainsADouble(int code)
    {
        var codeAsString = code.ToString();

        for (var i = 0; i < 5; i++)
        {
            if (codeAsString[i] == codeAsString[i + 1])
            {
                return true;
            }
        }

        return false;
    }

    private bool CountainsADoubleWhichisNotPartOfABiggerDouble(int code)
    {
        var codeAsString = code.ToString();

        for (var i = 0; i < 5; i++)
        {
            if (codeAsString[i] == codeAsString[i + 1])
            {
                if (i == 0 && codeAsString[i + 2] != codeAsString[i])
                {
                    return true;
                }

                if (i == 4 && codeAsString[i - 1] != codeAsString[i])
                {
                    return true;
                }

                if (i > 0
                 && i < 4
                 && codeAsString[i] != codeAsString[i - 1]
                 && codeAsString[i] != codeAsString[i + 2])
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool DigitsNeverDecrease(int code)
    {
        var codeAsInts = code.ToString()
                             .Select(c => int.Parse(c.ToString()))
                             .ToList();

        for (var i = 0; i < 5; i++)
        {
            if (codeAsInts[i + 1] < codeAsInts[i])
            {
                return false;
            }
        }

        return true;
    }
}