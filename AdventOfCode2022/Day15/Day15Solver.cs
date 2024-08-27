using System.Text.RegularExpressions;


namespace AdventOfCode2022.Day15;

public  class Day15Solver : SolverBase2022
{
    private  List<(int x, int y)> _beaconCoordinates;
    private  List<Sensor> _sensors;
    private  int _xMin;
    private  int _xMax;

    class Sensor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int BeaconX { get; set; }
        public int BeaconY { get; set; }

        public int DeltaX { get; set; }

        public Sensor(int x, int y, int beaconX, int beaconY)
        {
            X = x;
            Y = y;
            BeaconX = beaconX;
            BeaconY = beaconY;

            DeltaX = Math.Abs(x - beaconX) + Math.Abs(y - beaconY);
        }

        public int MinXAtY(int y) => X - DeltaX + Math.Abs(Y - y);
        public int MaxXAtY(int y) => X + DeltaX - Math.Abs(Y - y);
    }

    public  long SolvePuzzle1()
    {
        InitializePuzzle();
        return GetNumberOfImpossiblePlacesForALine(10);
    }
    
    public  long SolvePuzzle2()
    {
        InitializePuzzle();
        for (var x = 0; x <= 4000000; x++)
        {

            Console.WriteLine($"{x}");
            
            
            for (var y = 0; y <= 4000000; y++)
            {
                if (!IsLocationForbidden((x, y)))
                {
                    return (x * 4000000 + y);
                }
            }
        }

        throw new NotImplementedException();
    }

    private  void InitializePuzzle()
    {
        var inputs = LoadDataPerLineFromDay(15);
        _sensors = new List<Sensor>();
        _beaconCoordinates = new List<(int x, int y)>();

        foreach (var input in inputs)
        {
            var parsedInput = input.Split(" ");
            var sensorX = ExtractNumericValue(parsedInput[2]);
            var sensorY = ExtractNumericValue(parsedInput[3]);
            var beaconX = ExtractNumericValue(parsedInput[8]);
            var beaconY = ExtractNumericValue(parsedInput[9]);

            _beaconCoordinates.Add((beaconX, beaconY));
            _sensors.Add(new Sensor(sensorX, sensorY, beaconX, beaconY));
        }

        _xMin = _sensors.Min(s => s.X - s.DeltaX);
        _xMax = _sensors.Max(s => s.X + s.DeltaX);
    }

    private  long GetNumberOfImpossiblePlacesForALine(int row)
    {
        var answer = 0;
        for (var i = _xMin; i <= _xMax; i++)
        {
            var actualCoordinate = (i, row);
            //If we are under the distance between the closest sensor and it's closest beacon, then no sensor can be there
            if (IsLocationForbidden(actualCoordinate))
            {
                answer = answer + 1;
            }
        }

        return answer;
    }

    private  bool IsLocationForbidden((int x, int y) actualCoordinate)
    {
        if (_beaconCoordinates.Contains(actualCoordinate)) // There is a beacon so it an allowed location
        {
            return false;
        }

        foreach (var sensor in _sensors)
        {
            var minX = sensor.MinXAtY(actualCoordinate.y);
            var minY = sensor.MaxXAtY(actualCoordinate.y);
            
            if (actualCoordinate.x >= sensor.MinXAtY(actualCoordinate.y) && actualCoordinate.x <= sensor.MaxXAtY(actualCoordinate.y))
            {
                return true;
            }
        }

        return false;
    }

    private  int ExtractNumericValue(string input)
    {
        return int.Parse(Regex.Match(input, @"-?\d+").Value);
    }

    private  int GetManhattanDistance((int x, int y) first, (int x, int y) second)
    {
        return Math.Abs(second.x - first.x) + Math.Abs(second.y - first.y);
    }
}