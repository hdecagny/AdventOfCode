using AdventOfCode.Data;

namespace AdventOfCode.Day7;

public static class Day7Solver
{
    private static Dictionary<string, List<string>> _arborescence = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> _listOfFilesPerDir = new Dictionary<string, List<string>>();
    private static Dictionary<string, long> _sizeOfFilesPerDir = new Dictionary<string, long>();
    private static Dictionary<string, long> _totalSizePerDir = new Dictionary<string, long>();

    public static long SolvePuzzle1()
    {
        var data = DataLoader.LoadDataPerLineFromDay(7);
        _arborescence.Add("/", new List<string>());
        _listOfFilesPerDir.Add("/", new List<string>());
        _sizeOfFilesPerDir.Add("/", 0);
        ParseFiles(data);
        ComputeTotalSizePerDirectory();

        return _totalSizePerDir.Where(t => t.Value <= 100000).Sum(t => t.Value);
    }

    private static void ParseFiles(string[] data)
    {
        var actualDirectory = "/";
        var stringDepth = new List<string>();

        foreach (var command in data)
        {
            var parsedCommand = command.Split(" ");

            switch (parsedCommand[0])
            {
                case "$":
                {
                    if (parsedCommand[1] == "cd")
                    {
                        switch (parsedCommand[2])
                        {
                            case "/":
                                actualDirectory = "/";
                                stringDepth = new List<string>() { "/" };
                                break;
                            case "..":
                                stringDepth = stringDepth.SkipLast(1).ToList();
                                actualDirectory = string.Join("/", stringDepth);
                                break;
                            default:
                                var newDirectory = actualDirectory + "/" + parsedCommand[2];
                                actualDirectory = newDirectory;
                                stringDepth.Add(parsedCommand[2]);
                                break;
                        }
                    }

                    break;
                }
                case "dir":
                {
                    var listOfDir = _arborescence[actualDirectory];
                    var newDirectory = actualDirectory + "/" + parsedCommand[1];

                    if (!listOfDir.Contains(newDirectory))
                    {
                        listOfDir.Add(newDirectory);
                    }

                    if (!_arborescence.ContainsKey(newDirectory))
                    {
                        _arborescence.Add(newDirectory, new List<string>());
                    }

                    if (!_listOfFilesPerDir.ContainsKey(newDirectory))
                    {
                        _listOfFilesPerDir.Add(newDirectory, new List<string>());
                    }

                    if (!_sizeOfFilesPerDir.ContainsKey(newDirectory))
                    {
                        _sizeOfFilesPerDir.Add(newDirectory, 0);
                    }

                    break;
                }
                default:
                {
                    var filename = parsedCommand[1];
                    var filesize = long.Parse(parsedCommand[0]);

                    var listOfFiles = _listOfFilesPerDir[actualDirectory];
                    var actualsize = _sizeOfFilesPerDir[actualDirectory];

                    if (!listOfFiles.Contains(filename))
                    {
                        listOfFiles.Add(filename);
                        _sizeOfFilesPerDir[actualDirectory] = actualsize + filesize;
                    }

                    break;
                }
            }
        }
    }

    private static void ComputeTotalSizePerDirectory()
    {
        var test = _arborescence.Where(kpv => kpv.Value.Count > 10);

        foreach (var directory in _arborescence.Keys.OrderBy(k => _arborescence[k].Count))
        {
            GetTotalMemoryForDir(directory);
        }
    }

    private static long GetTotalMemoryForDir(string directory)
    {
        if (_totalSizePerDir.ContainsKey(directory))
        {
            return _totalSizePerDir[directory];
        }

        var totalsize = _sizeOfFilesPerDir[directory];

        foreach (var innerDir in _arborescence[directory])
        {
            if (_totalSizePerDir.ContainsKey(innerDir))
            {
                totalsize += _totalSizePerDir[innerDir];
            }
            else
            {
                totalsize += GetTotalMemoryForDir(innerDir);
            }
        }

        _totalSizePerDir.Add(directory, totalsize);

        return totalsize;
    }

    public static double SolvePuzzle2()
    {
        var data = DataLoader.LoadDataPerLineFromDay(7);
        _arborescence.Add("/", new List<string>());
        _listOfFilesPerDir.Add("/", new List<string>());
        _sizeOfFilesPerDir.Add("/", 0);
        ParseFiles(data);
        ComputeTotalSizePerDirectory();

        var totalavailablesize = 70000000;
        var wishedsize = totalavailablesize - 30000000;
        var occupiedMemory = _totalSizePerDir["/"];
        var minMemoryToDelete = occupiedMemory - wishedsize;


        return _totalSizePerDir.Where(t => t.Value >= minMemoryToDelete)
            .Min(t => t.Value);
    }
}