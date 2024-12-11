namespace AdventOfCode2024;

public class Day9Solver : SolverBase2024
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataFromDay(9).Select(c => int.Parse(c.ToString())).ToList();
        var length = data.Count;

        var filledGroups = Enumerable.Range(0, length)
            .Where(i => i % 2 == 0)
            .Select(i => data[i])
            .ToList();
        
        var idOrdered = GetIdNumberOrdered(filledGroups);
        var refragementedfile = GetRefragmentedFile(idOrdered, data);

        return refragementedfile.Sum(kvp=> kvp.Key * kvp.Value);
    }
    
    public override double SolvePuzzle2()
    {
        var data = LoadDataFromDay(9).Select(c => int.Parse(c.ToString())).ToList();
        var length = data.Count;

        var filledGroups = Enumerable.Range(0, length)
            .Where(i => i % 2 == 0)
            .Select(i => data[i])
            .ToList();
        
        var idOrdered = GetIdNumberOrdered(filledGroups);
        var refragementedfile = GetRefragmentedFile2(idOrdered, data);

        return refragementedfile.Sum(kvp=> kvp.Key * kvp.Value);
    }

    private Dictionary<long, long> GetIdNumberOrdered(List<int> filledGroups)
    {
        List<(int size, int index)> sizeAndIndex = filledGroups.Zip(Enumerable.Range(0, filledGroups.Count)).ToList();

        var filesOrdered = new Dictionary<long, long>();

        var i = 0;

        foreach (var group in sizeAndIndex)
        {
            for (var j = 0; j < group.size; j++)
            {
                filesOrdered.Add(i, group.index);
                i += 1;
            }
        }

        return filesOrdered;
    }

    private Dictionary<long, long> GetRefragmentedFile(Dictionary<long, long> idOrdered, List<int> input)
    {
        var fileamount = idOrdered.Count;
        var refragmented = new Dictionary<long, long>();
        var indexStart = 0;
        var indexReverted = fileamount - 1;
        var currentIndex = 0;
        var groupnumber = 0;

        while (refragmented.Count < fileamount)
        {
            if (groupnumber % 2 == 0)
            {
                for (var i = 0; i < input[groupnumber]; i++)
                {
                    refragmented.Add(currentIndex, idOrdered[indexStart]);
                    indexStart += 1;
                    currentIndex += 1;
                } 
            }
            else
            {
                for (var i = 0; i < input[groupnumber]; i++)
                {
                    refragmented.Add(currentIndex, idOrdered[indexReverted]);
                    indexReverted -= 1;
                    currentIndex += 1;
                } 
            }
            
            groupnumber += 1;
        }

        return refragmented.Where(r=>r.Key < fileamount).ToDictionary(r=>r.Key, r=>r.Value);
    }
    
    private Dictionary<long, long> GetRefragmentedFile2(Dictionary<long, long> idOrdered, List<int> input)
    {
        throw new NotImplementedException();
    }

    private Dictionary<long, int?> CreateInitialFileDictionnary(List<int> input)
    {
        var dictionary = new Dictionary<long, int?>();
        var groupnumber = 0;
        var index = 0;

        foreach (var groupsize in input)
        {
            if (groupnumber % 2 == 0)
            {
                for (var i = 0; i < input[groupnumber]; i++)
                {
                    dictionary.Add(index, groupnumber);
                    index += 1;
                }
                
                groupnumber += 1;
            }
            else
            {
                for (var i = 0; i < input[groupnumber]; i++)
                {
                    dictionary.Add(index, null);
                    index += 1;
                }
            }
        }

        return dictionary;
    }
}