namespace AdventOfCode2022.Day11;

public class Monkey
{
    public int MonkeyId { get; }

    public Func<long, long> InspectionOperation { get; }

    public Func<long, int> WorrinessTest { get; }

    public Queue<long> ListOfItems { get; }

    public long MonkeyBusiness { get; set; }
    
    public int Diviser { get; }

    public Monkey(string input)
    {
        var parsedinput = input.Split("\r\n");

        MonkeyId = int.Parse(parsedinput[0][7].ToString());
        
        var startingItems = parsedinput[1].Split(": ")[1].Split(", ");
        var parsedItems = startingItems.Select(long.Parse);
        ListOfItems = new Queue<long>(parsedItems);

        var inspectionOperationString = parsedinput[2];
        InspectionOperation = GetInspectionOperation(inspectionOperationString);

        var divisibility = int.Parse(parsedinput[3].Split(" ").Last());
        Diviser = divisibility;
        var monkeyTrue = int.Parse(parsedinput[4].Split(" ").Last());
        var monkeyFalse = int.Parse(parsedinput[5].Split(" ").Last());
        WorrinessTest = (x) => x % divisibility == 0 ? monkeyTrue : monkeyFalse;

        MonkeyBusiness = 0;
    }

    private static Func<long, long> GetInspectionOperation(string inspectionOperationString)
    {
        var operations = inspectionOperationString.Split(" ");
        var operationLength = operations.Length;

        if (operations.Last() == "old")
        {
            return i => i * i;
        }

        switch (operations[operationLength - 2])
        {
            case "*":
            {
                var number = int.Parse(operations.Last());
                return i => i * number;
            }
            case "+":
            {
                var number = int.Parse(operations.Last());
                return i => i + number;
            }
            default:
                throw new NotImplementedException();
        }
    }
}