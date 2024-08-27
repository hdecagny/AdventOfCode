

namespace AdventOfCode2022.Day12;

public  class Day12Solver : SolverBase2022
{
    private  char[][] _input;
    private  (int height, int width) _entranceCoordinates;
    private  (int height, int width) _exitCoordinates;
    private  int _mapHeight;
    private  int _mapWidth;

    private  HashSet<(int, int)> _unvisitedLocations = new HashSet<(int, int)>();
    private  Dictionary<(int, int), int> _minimalEffortPerLocation = new Dictionary<(int, int), int>();
    private  int _numberOfTrial =0;

    public  long SolvePuzzle1()
    {
        _input = LoadDataPerLineFromDay(12)
            .Select(l => l.ToArray())
            .ToArray();

        var entranceHeight = Array.FindIndex(_input, i => i.Contains('S'));
        var entranceWidth = Array.FindIndex(_input[entranceHeight], i => i == 'S');
        var maxEffort = 100000;
        _entranceCoordinates = (entranceHeight, entranceWidth);

        var exitHeight = Array.FindIndex(_input, i => i.Contains('E'));
        var exitWidth = Array.FindIndex(_input[exitHeight], i => i == 'E');
        _exitCoordinates = (exitHeight, exitWidth);

        _mapHeight = _input.Length;
        _mapWidth = _input[0].Length;
        

        InitializeEffort(maxEffort);

        var currentLocation = _entranceCoordinates;
        _minimalEffortPerLocation[_entranceCoordinates] = 0;
        _unvisitedLocations.Remove(_entranceCoordinates);

        while (currentLocation!= _exitCoordinates)
        {
            KeepExploring(currentLocation, _minimalEffortPerLocation[currentLocation]);
            var nextLocation = _unvisitedLocations.MinBy(x => _minimalEffortPerLocation[x]);
            currentLocation = nextLocation;
            _unvisitedLocations.Remove(currentLocation);
        }

        return _minimalEffortPerLocation[_exitCoordinates];
    }

    private  void InitializeEffort(int maxEffort)
    {
        _unvisitedLocations = new HashSet<(int, int)>();
        _minimalEffortPerLocation = new Dictionary<(int, int), int>();

        for (var i = 0; i <= _mapHeight - 1; i++)
        {
            for (var j = 0; j <= _mapWidth - 1; j++)
            {
                _unvisitedLocations.Add((i, j));
                _minimalEffortPerLocation.Add((i, j), maxEffort);
            }
        }
    }

    public  long SolvePuzzle2()
    {
        _input = LoadDataPerLineFromDay(12)
            .Select(l => l.ToArray())
            .ToArray();
        
        var entranceHeight = Array.FindIndex(_input, i => i.Contains('S'));
        var entranceWidth = Array.FindIndex(_input[entranceHeight], i => i == 'S');
        _entranceCoordinates = (entranceHeight, entranceWidth);

        var exitHeight = Array.FindIndex(_input, i => i.Contains('E'));
        var exitWidth = Array.FindIndex(_input[exitHeight], i => i == 'E');
        _exitCoordinates = (exitHeight, exitWidth);

        _mapHeight = _input.Length;
        _mapWidth = _input[0].Length;

        var possibleStarts = new List<(int, int)>();
        
        for (var i = 0; i <= _mapHeight - 1; i++)
        {
            for (var j = 0; j <= _mapWidth - 1; j++)
            {
                if (_input[i][j] == 'a' || _input[i][j] == 'S')
                {
                    possibleStarts.Add((i,j));
                } 
            }
        }

        return possibleStarts.Min(CalculateMinimumEffort);
    }

    private  int CalculateMinimumEffort((int, int) startingPath)
    {
        InitializeEffort(10000);
        var currentLocation = startingPath;
        _minimalEffortPerLocation[currentLocation] = 0;
        _unvisitedLocations.Remove(currentLocation);

        while (currentLocation!= _exitCoordinates)
        {
            KeepExploring(currentLocation, _minimalEffortPerLocation[currentLocation]);
            var nextLocation = _unvisitedLocations.MinBy(x => _minimalEffortPerLocation[x]);
            currentLocation = nextLocation;
            _unvisitedLocations.Remove(currentLocation);
        }

        _numberOfTrial += 1;
        if (_numberOfTrial % 100 ==0)
        {
            Console.WriteLine($"trial :{_numberOfTrial} ");
        }

        return _minimalEffortPerLocation[_exitCoordinates];
    }

    private  void KeepExploring((int, int) currentPath, int currentEffort)
    {
        LookNorth(currentPath, currentEffort);
        LookSouth(currentPath, currentEffort);
        LookWest(currentPath, currentEffort);
        LookEast(currentPath, currentEffort);
    }

    private  void LookNorth((int height, int width) currentTile, int currentEffort)
    {
        var tileNorth = (currentTile.height - 1, currentTile.width);

        if (currentTile.height - 1 < 0
            || !_unvisitedLocations.Contains(tileNorth)
            || !IsReachable(currentTile, tileNorth))
        {
            return;
        }

        _minimalEffortPerLocation[tileNorth] = Math.Min(_minimalEffortPerLocation[tileNorth], currentEffort + 1);
    }

    private  void LookSouth((int height, int width) currentTile, int currentEffort)
    {
        var tilesouth = (currentTile.height + 1, currentTile.width);

        if (currentTile.height + 1 > _mapHeight - 1
            || !_unvisitedLocations.Contains(tilesouth)
            || !IsReachable(currentTile, tilesouth))
        {
            return;
        }

        _minimalEffortPerLocation[tilesouth] = Math.Min(_minimalEffortPerLocation[tilesouth], currentEffort + 1);
    }

    private  void LookWest((int height, int width) currentTile, int currentEffort)
    {
        var tileWest = (currentTile.height, currentTile.width - 1);

        if (currentTile.width - 1 < 0
            || !_unvisitedLocations.Contains(tileWest)
            || !IsReachable(currentTile, tileWest))
        {
            return;
        }

        _minimalEffortPerLocation[tileWest] = Math.Min(_minimalEffortPerLocation[tileWest], currentEffort + 1);
    }

    private  void LookEast((int height, int width) currentTile, int currentEffort)
    {
        var tileEast = (currentTile.height, currentTile.width + 1);

        if (currentTile.width + 1 > _mapWidth - 1
            || !_unvisitedLocations.Contains(tileEast)
            || !IsReachable(currentTile, tileEast))
        {
            return;
        }

        _minimalEffortPerLocation[tileEast] = Math.Min(_minimalEffortPerLocation[tileEast], currentEffort + 1);
    }

    private  int GetHeight(char character)
    {
        switch (character)
        {
            case 'S':
                return (int)'a';
            case 'E':
                return (int)'z';
            default:
                return character;
        }
    }

    private  bool IsReachable((int height, int width) currentTileCoordinate, (int height, int width) nextTileCoordinate)
    {
        var currentHeight = GetHeight(_input[currentTileCoordinate.height][currentTileCoordinate.width]);
        var nextHeight = GetHeight(_input[nextTileCoordinate.height][nextTileCoordinate.width]);

        return nextHeight - 1 <= currentHeight;
    }
}