using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Data
{
    public class DataLoader
    {
        public static string[] LoadDataPerLineFromDay(int day)
        {
            return System.IO.File.ReadAllLines(@$"C:\Users\henri\Source\Repos\hdecagny\AdventOfCode2021\Aoc\Data\Day{day}Data.txt");
        }

        public static string LoadDataFromDay(int day)
        {
            return System.IO.File.ReadAllText(@$"C:\Users\henri\Source\Repos\hdecagny\AdventOfCode2021\Aoc\Data\Day{day}Data.txt");
        }
    }
}
