

namespace AdventOfCode.Day12;

public static class Day12Solver
{
    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(12)
            .Select(s => s.Split('-'))
            .ToList();

        var connectionsPerRoom = GetConnectionsPerRoom(data);

        var stack = new Stack<List<string>>();
        stack.Push(new List<string> { "start" });

        var validPaths = new List<List<string>>();

        while (stack.TryPop(out var currentpath))
        {
            var possibleconnections = connectionsPerRoom[currentpath.Last()];

            foreach (var newRoom in possibleconnections.Where(newRoom => newRoom != "start"))
            {
                if (newRoom == "end")
                {
                    var newPath = currentpath;
                    newPath = newPath.Append(newRoom).ToList();
                    validPaths.Add(newPath);
                }

                else if (char.IsUpper(newRoom[0]))
                {
                    var newPath = currentpath;
                    newPath = newPath.Append(newRoom).ToList();
                    stack.Push(newPath);
                }

                else if (char.IsLower(newRoom[0]) && !currentpath.Contains(newRoom))
                {
                    var newPath = currentpath;
                    newPath = newPath.Append(newRoom).ToList();
                    stack.Push(newPath);
                }
            }
        }

        return validPaths.Count;
    }

    private static Dictionary<string, List<string>> GetConnectionsPerRoom(List<string[]> data)
    {
        var answer = new Dictionary<string, List<string>>();

        foreach (var line in data)
        {
            if (!answer.ContainsKey(line[0]))
            {
                answer.Add(line[0], new List<string> { line[1] });
            }
            else
            {
                answer[line[0]].Add(line[1]);
            }

            if (!answer.ContainsKey(line[1]))
            {
                answer.Add(line[1], new List<string> { line[0] });
            }
            else
            {
                answer[line[1]].Add(line[0]);
            }
        }

        return answer;
    }

    public static long SolvePuzzle2()
    {
        var data = DataLoader.LoadDataPerLineFromDay(12)
            .Select(s => s.Split('-'))
            .ToList();

        var connectionsPerRoom = GetConnectionsPerRoom(data);

        var stack = new Stack<List<string>>();
        stack.Push(new List<string> { "start" });

        var validPaths = new List<List<string>>();

        while (stack.TryPop(out var currentpath))
        {
            var possibleconnections = connectionsPerRoom[currentpath.Last()];

            foreach (var newRoom in possibleconnections.Where(newRoom => newRoom != "start"))
            {
                if (newRoom == "end")
                {
                    var newPath = currentpath;
                    newPath = newPath.Append(newRoom).ToList();
                    validPaths.Add(newPath);
                }

                else if (char.IsUpper(newRoom[0]))
                {
                    var newPath = currentpath;
                    newPath = newPath.Append(newRoom).ToList();
                    stack.Push(newPath);
                }

                else if (char.IsLower(newRoom[0])
                         && (!currentpath.Contains(newRoom) || !WasASmallCaveVisitedTwice(currentpath)))

                {
                    var newPath = currentpath;
                    newPath = newPath.Append(newRoom).ToList();
                    stack.Push(newPath);
                }
            }
        }

        validPaths = validPaths.OrderBy(s => s.Count).ToList();

        return validPaths.Count;
    }

    private static bool WasASmallCaveVisitedTwice(List<string> path)
    {
        var smallcaveCount = path.Count(s => char.IsLower(s[0]));
        var smallcaveDistinctCount = path
            .Where(s => char.IsLower(s[0]))
            .Distinct()
            .Count();


        return smallcaveCount != smallcaveDistinctCount;
    }
}