using System.Text.Json;
using System.Text.Json.Nodes;
using AdventOfCode.Data;

namespace AdventOfCode2022.Day13;

public static class Day13Solver
{
    public static long SolvePuzzle1()
    {
        var input = DataLoader.LoadDataFromDay(13);

        var pairOfPackets = input.Split("\r\n\r\n");

        var answer = 0;
        
        for (var i=0; i<pairOfPackets.Length; i++)
        {
            var packets = pairOfPackets[i].Split("\r\n");
            var leftPacket = JsonNode.Parse(packets[0]);
            var rightPacket = JsonNode.Parse(packets[1]);
            if (ComparePackets(leftPacket, rightPacket).Value)
            {
                answer = answer + i + 1;
            }
        }

        return answer;
    }

    private static bool? ComparePackets(JsonNode leftPacket, JsonNode rightPacket)
    {
        if (leftPacket is JsonValue leftVal && rightPacket is JsonValue rightVal)
        {
            var leftInt = leftVal.GetValue<int>();
            var rightInt = rightVal.GetValue<int>();
            return leftInt == rightInt ? null : leftInt < rightInt;
        }

        if (leftPacket is not JsonArray leftArray)
        {
            leftArray = new JsonArray(leftPacket.GetValue<int>());
        }

        if (rightPacket is not JsonArray rightArray)
        {
            rightArray = new JsonArray(rightPacket.GetValue<int>());
        }

        var minimumLength = Math.Min(leftArray.Count, rightArray.Count);

        for (var i = 0; i < minimumLength; i++)
        {
            var res = ComparePackets(leftArray[i], rightArray[i]);
            if (res.HasValue) { return res.Value; }
        }

        if (leftArray.Count < rightArray.Count) { return true; }
        if (leftArray.Count > rightArray.Count) { return false; }
 
        return null;
    }
    
    public static long SolvePuzzle2()
    {
        var input = DataLoader.LoadDataFromDay(13);

        var allPackets = input.Split("\r\n\r\n").SelectMany(i => i.Split("\r\n")).ToList();
        allPackets.Add("[[2]]");
        allPackets.Add("[[6]]");

        var jsonNodes = allPackets.Select(p => JsonNode.Parse(p)).ToList();
        jsonNodes.Sort((left, right) => ComparePackets(left, right).Value ? -1 : 1);

        var test = jsonNodes.Select(i => i.ToString()).ToList();
        var firstIndex = jsonNodes.FindIndex(i => i.ToString() ==JsonNode.Parse("[[2]]").ToString());
        var secondIndex = jsonNodes.FindIndex(i => i.ToString() ==JsonNode.Parse("[[6]]").ToString());
        
        return (firstIndex+1) * (secondIndex+1);
    }
}
    