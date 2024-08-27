namespace AdventOfCode2023;

using System.Text.RegularExpressions;

public class Day2Solver : SolverBase2023
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataPerLineFromDay(2);

        var parsedgames = data.Select(d => d.Split(':', ';'));

        return parsedgames.Select(GetGameIsValid).Sum();
    }

    private int GetGameIsValid(string[] game)
    {
        var gameid = int.Parse(game[0].Split(' ').Last());

        foreach (var trial in game.Skip(1))
        {
            var amountOfCubePerColourDictionnary = trial.Split(',')
                                                        .Select(s => s[1..].Split(' '))
                                                        .ToDictionary(l => l[1], l => int.Parse(l[0]));

            foreach (var amountOfCubePerColour in amountOfCubePerColourDictionnary)
            {
                if ((amountOfCubePerColour.Key == "red" && amountOfCubePerColour.Value > 12)
                 || (amountOfCubePerColour.Key == "green" && amountOfCubePerColour.Value > 13)
                 || (amountOfCubePerColour.Key == "blue" && amountOfCubePerColour.Value > 14))
                {
                    return 0;
                }
            }
        }

        return gameid;
    }

    public override double SolvePuzzle2()
    {
        var data = LoadDataPerLineFromDay(2);

        var parsedgames = data.Select(d => d.Split(':', ';'));

        return parsedgames.Select(GetGamePower).Sum();
    }

    private long GetGamePower(string[] game)
    {
        var minRedCubes = 0;
        var minBlueCubes = 0;
        var minGreenCubes = 0;

        foreach (var trial in game.Skip(1))
        {
            var amountOfCubePerColourDictionnary = trial.Split(',')
                                                        .Select(s => s[1..].Split(' '))
                                                        .ToDictionary(l => l[1], l => int.Parse(l[0]));

            foreach (var kpg in amountOfCubePerColourDictionnary)
            {
                if (kpg.Key == "red")
                {
                    minRedCubes = Math.Max(minRedCubes, kpg.Value);
                }

                if (kpg.Key == "blue")
                {
                    minBlueCubes = Math.Max(minBlueCubes, kpg.Value);
                }

                if (kpg.Key == "green")
                {
                    minGreenCubes = Math.Max(minGreenCubes, kpg.Value);
                }
            }
        }

        var answer = minRedCubes * minGreenCubes * minBlueCubes;

        return answer;
    }
}