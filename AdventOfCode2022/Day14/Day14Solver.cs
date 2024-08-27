using System.Text.Json.Nodes;


namespace AdventOfCode2022.Day14;

public  class Day14Solver : SolverBase2022
{
    private  string _input;
    private  int _xMin;
    private  int _xMax;
    private  int _yMax;
    private  (int, int) _waterfall = (500, 0);
    private  int _grainsofSands;

    private  Dictionary<(int x, int y), string> _map;

    public  long SolvePuzzle1()
    {
        _input = LoadDataFromDay(14);
        InitializePuzzle();
        LetTheSandFlow(_waterfall.Item1, _waterfall.Item2);
        DrawMap();

        return _grainsofSands;
    }

    private  void InitializePuzzle()
    {
        var inputPerLine = _input.Split("\r\n");
        var allcoordinates = inputPerLine.SelectMany(i => i.Split(" -> "));

        var allXcoordinates = allcoordinates.Select(c => int.Parse(c.Split(",")[0])).ToList();
        var allycoordinates = allcoordinates.Select(c => int.Parse(c.Split(",")[1])).ToList();

        _xMin = allXcoordinates.Min();
        _xMax = allXcoordinates.Max();
        _yMax = allycoordinates.Max();

        _map = new Dictionary<(int x, int y), string>();
        _grainsofSands = 0;

        InitializeMap();
    }

    private  void InitializeMap()
    {
        for (var x = _xMin; x <= _xMax; x++)
        {
            for (var y = 0; y <= _yMax; y++)
            {
                _map.Add((x, y), ".");
            }
        }

        var inputPerLine = _input.Split("\r\n");

        foreach (var line in inputPerLine)
        {
            var coordinates = line.Split(" -> ")
                .Select(c => c.Split(","))
                .Select(d => (int.Parse(d[0]), int.Parse(d[1])))
                .ToList();

            var length = coordinates.Count();

            for (var i = 0; i < length - 1; i++)
            {
                CreateLine(coordinates[i], coordinates[i + 1]);
            }
        }
    }

    private  void CreateLine((int x, int y) first, (int x, int y) second)
    {
        if (first.x == second.x)
        {
            var ymin = Math.Min(first.y, second.y);
            var ymax = Math.Max(first.y, second.y);
            var xCoordinate = first.x;

            for (var y = ymin; y <= ymax; y++)
            {
                _map[(xCoordinate, y)] = "#";
            }
        }
        else
        {
            var xmin = Math.Min(first.x, second.x);
            var xmax = Math.Max(first.x, second.x);
            var yCoordinate = first.y;

            for (var x = xmin; x <= xmax; x++)
            {
                _map[(x, yCoordinate)] = "#";
            }
        }
    }

    private  void DrawMap()
    {
        for (var y = 0; y <= _yMax; y++)
        {
            Console.Write($"{y}");

            for (var x = _xMin; x <= _xMax; x++)
            {
                Console.Write($"{_map[(x, y)]}");
            }

            Console.WriteLine("");
        }
    }

    private  void LetTheSandFlow(int x, int y)
    {
        while (true)
        {
            //Check if we are outside the boundaries and break if yes
            if (x < _xMin || x > _xMax || y + 1 > _yMax)
            {
                return;
            }


            switch (_map[(x, y + 1)])
            {
                case ".":
                    y = y + 1;
                    continue;
                case "#":
                case "o":
                    if (x - 1 < _xMin)
                    {
                        return;
                    }

                    switch (_map[(x - 1, y + 1)])
                    {
                        case ".":
                            x = x - 1;
                            y = y + 1;
                            continue;
                        case "o":
                        case "#":
                            if (x + 1 > _xMax)
                            {
                                return;
                            }

                            switch (_map[(x + 1, y + 1)])
                            {
                                case ".":
                                    x = x + 1;
                                    y = y + 1;
                                    continue;
                                case "o":
                                case "#":
                                    UpdateMapWithSand(x, y);
                                    x = _waterfall.Item1;
                                    y = _waterfall.Item2;
                                    continue;
                            }

                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    break;
            }

            break;
        }
    }

    private  void UpdateMapWithSand(int x, int y)
    {
        _map[(x, y)] = "o";
        _grainsofSands += 1;
    }

    public  long SolvePuzzle2()
    {
        _input = LoadDataFromDay(14);
        InitializePuzzle2();
        LetTheSandFlow2(_waterfall.Item1, _waterfall.Item2);
        DrawMap();

        return _grainsofSands;
    }

    private  void InitializePuzzle2()
    {
        var inputPerLine = _input.Split("\r\n");
        var allcoordinates = inputPerLine.SelectMany(i => i.Split(" -> "));

        var allycoordinates = allcoordinates.Select(c => int.Parse(c.Split(",")[1])).ToList();

        _xMin = 300;
        _xMax = 700;
        _yMax = allycoordinates.Max();
        _yMax += 2;

        _map = new Dictionary<(int x, int y), string>();
        _grainsofSands = 0;

        InitializeMap2();
    }

    private  void InitializeMap2()
    {
        for (var x = _xMin; x <= _xMax; x++)
        {
            for (var y = 0; y <= _yMax; y++)
            {
                _map.Add((x, y), ".");
            }

            _map[(x, _yMax)]= "#";
        }

        var inputPerLine = _input.Split("\r\n");

        foreach (var line in inputPerLine)
        {
            var coordinates = line.Split(" -> ")
                .Select(c => c.Split(","))
                .Select(d => (int.Parse(d[0]), int.Parse(d[1])))
                .ToList();

            var length = coordinates.Count();

            for (var i = 0; i < length - 1; i++)
            {
                CreateLine(coordinates[i], coordinates[i + 1]);
            }
        }
    }

    private  void LetTheSandFlow2(int x, int y)
    {
        while (true)
        {
            //Check if we are outside the boundaries and break if yes
            if (_map[_waterfall] == "o")
            {
                return;
            }

            switch (_map[(x, y + 1)])
            {
                case ".":
                    y += 1;
                    continue;
                case "#":
                case "o":
                    if (x - 1 < _xMin)
                    {
                        return;
                    }

                    switch (_map[(x - 1, y + 1)])
                    {
                        case ".":
                            x -= 1;
                            y += 1;
                            continue;
                        case "o":
                        case "#":
                            if (x + 1 > _xMax)
                            {
                                return;
                            }

                            switch (_map[(x + 1, y + 1)])
                            {
                                case ".":
                                    x += 1;
                                    y += 1;
                                    continue;
                                case "o":
                                case "#":
                                    UpdateMapWithSand(x, y);
                                    x = _waterfall.Item1;
                                    y = _waterfall.Item2;
                                    continue;
                            }

                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    break;
            }

            break;
        }
    }
}