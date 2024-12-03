namespace AdventOfCode2019;

public class Day8Solver : SolverBase2019
{
    public override double SolvePuzzle1()
    {
        var data = LoadDataFromDay(8);
        var wide = 25;
        var height = 6;
        var totalDigit = data.Length;
        var layerlength = wide * height;
        var numberOfLayers = totalDigit / layerlength;


        var minZeroes = 100;
        var result = 0;

        for (var i = 0; i < numberOfLayers; i++)
        {
            var layer = data.Substring(i * layerlength, layerlength);
            var amountOfZeros = layer.Count(c => c == '0');

            if (amountOfZeros < minZeroes)
            {
                result = layer.Count(c => c == '1') * layer.Count(c => c == '2');
                minZeroes = amountOfZeros;
            }
        }

        return result;
    }

    public override double SolvePuzzle2()
    {
        var data = LoadDataFromDay(8);
        var wide = 25;
        var height = 6;
        var numberOfPixels = wide * height;

        var pixelRange = Enumerable.Range(0, numberOfPixels);
        char[] test1 = [data[0], data[150], data[300], data[450], data[600]];


        var pixelvalues = pixelRange.Select(pix => data.Where((x, i) => i % numberOfPixels == pix && x != '2').First()).ToList();
        for (var i = 0; i < height; i++)
        {
            Console.WriteLine(new string(pixelvalues.Slice(i * wide, wide).Select(j => j == '0' ? ' ' : '1').ToArray()));
        }

        Console.ReadLine();

        return 100;
    }
}