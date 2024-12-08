namespace AdventOfCode2024;

public class Day7Solver : SolverBase2024
{
    private readonly Dictionary<int, List<List<Operators>>> _operatorPermutationsPerSize = new();

    public override double SolvePuzzle1()
    {
        var input = LoadDataPerLineFromDay(7).Select(s => s.Split(": ")).ToList();
        var answer = input.Where(l => CanEquationBeTrue(l[0], l[1]))
            .Sum(l => long.Parse(l[0]));

        return answer;
    }

    public override double SolvePuzzle2()
    {
        var input = LoadDataPerLineFromDay(7).Select(s => s.Split(": ")).ToList();

        var test = input.Where(l => CanEquationBeTrue2(l[0], l[1])).ToList();
        
        var answer = input.Where(l => CanEquationBeTrue2(l[0], l[1]))
            .Sum(l => long.Parse(l[0]));

        return answer;
    }

    private bool CanEquationBeTrue(string result, string numbersListAsString)
    {
        var resultLong = long.Parse(result);
        var numbersList = numbersListAsString.Split(' ').Select(long.Parse).ToList();

        var listSize = numbersList.Count;
        var operatorCompinations = GetOperatorPermutationForGivenSize(listSize - 1);

        return operatorCompinations.Any(o => IsEquationValid(resultLong, numbersList, o));
    }
    
    private bool CanEquationBeTrue2(string result, string numbersListAsString)
    {
        var resultLong = long.Parse(result);
        var numbersList = numbersListAsString.Split(' ').Select(long.Parse).ToList();

        var listSize = numbersList.Count;
        var operatorCompinations = GetOperatorPermutationForGivenSize2(listSize - 1);

        return operatorCompinations.Any(o => IsEquationValid(resultLong, numbersList, o));
    }

    private bool IsEquationValid(long finalResult, List<long> numbersList, List<Operators> operatorList)
    {
        if (finalResult == 192)
        {
            var test = 0;
        }
        
        var result = numbersList[0];

        for (var i = 0; i < numbersList.Count-1; i++)
        {
            switch (operatorList[i])
            {
                case Operators.Addition:
                    result += numbersList[i + 1];
                    break;
                case Operators.Multiplication:
                    result *= numbersList[i + 1];
                    break;
                case Operators.Concatenation:
                    result = long.Parse(result.ToString() + numbersList[i + 1].ToString());
                    break;   
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (result > finalResult)
            {
                return false;
            }
        }


        return finalResult == result;
    }

    private List<List<Operators>> GetOperatorPermutationForGivenSize(int size)
    {
        if (size == 1)
        {
            return [[Operators.Addition], [Operators.Multiplication]];
        }

        if (_operatorPermutationsPerSize.TryGetValue(size, out var givenSize))
        {
            return givenSize;
        }

        var listSizeBelow = GetOperatorPermutationForGivenSize(size - 1);

        var answer = listSizeBelow.SelectMany(p => (List<List<Operators>>) [p.Concat([Operators.Addition]).ToList(), p.Concat([Operators.Multiplication]).ToList()])
            .ToList();
        _operatorPermutationsPerSize.Add(size, answer);

        return answer;
    }
    
    private List<List<Operators>> GetOperatorPermutationForGivenSize2(int size)
    {
        if (size == 1)
        {
            return [[Operators.Addition], [Operators.Multiplication], [Operators.Concatenation]];
        }

        if (_operatorPermutationsPerSize.TryGetValue(size, out var givenSize))
        {
            return givenSize;
        }

        var listSizeBelow = GetOperatorPermutationForGivenSize2(size - 1);

        var answer = listSizeBelow.SelectMany(p => (List<List<Operators>>) [p.Concat([Operators.Addition]).ToList(), p.Concat([Operators.Multiplication]).ToList(), p.Concat([Operators.Concatenation]).ToList()])
            .ToList();
        _operatorPermutationsPerSize.Add(size, answer);

        return answer;
    }


    private enum Operators
    {
        Addition,
        Multiplication,
        Concatenation,
    }
}