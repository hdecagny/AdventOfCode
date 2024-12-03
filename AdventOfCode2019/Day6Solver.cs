namespace AdventOfCode2019;

public class Day6Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataPerLineFromDay(6);

        var orbits = data
            .Select(d => d.Split(')'))
            .ToDictionary(s => s[1], s => s[0]);

        var answer = orbits.Select(o => CalculateChain(orbits, o.Key)).Sum();

        return answer;
    }

    private int CalculateChain(Dictionary<string, string> orbits, string planet)
    {
        var result = 0;
        var currentplanet = planet;

        while (currentplanet != "COM")
        {
            result += 1;
            currentplanet = orbits[currentplanet];
        }

        return result;
    }

    public override double SolvePuzzle2()
    {
        var data = LoadDataPerLineFromDay(6);

        var orbits = data
            .Select(d => d.Split(')'))
            .ToDictionary(s => s[1], s => s[0]);

        var startingorbit = orbits["YOU"];
        var finishorbit = orbits["SAN"];

        var possibilities = new Queue<List<string>>();
        var visitedplaces = new List<string> { startingorbit, "YOU", "COM" };

        possibilities.Enqueue([startingorbit]);

        while (possibilities.Count != 0)
        {
            var nextitinerary = possibilities.Dequeue();
            var actualposition = nextitinerary.Last();
            visitedplaces.Add(actualposition);

            var placesToVisit = orbits.Where(kpv => kpv.Value == actualposition)
                .Select(kpg => kpg.Key)
                .Distinct()
                .ToList();

            placesToVisit.Add(orbits[actualposition]);
            placesToVisit = placesToVisit.Where(p => !visitedplaces.Contains(p)).ToList();

            if (placesToVisit.Contains(finishorbit))
            {
                return nextitinerary.Count;
            }

            foreach (var placetovisit in placesToVisit)
            {
                possibilities.Enqueue(nextitinerary.Concat([placetovisit]).ToList());
            }
        }

        return -1000;
    }
}