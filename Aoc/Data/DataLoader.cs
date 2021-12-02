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
            return System.IO.File.ReadAllLines(@$"C:\Users\Hcagny\source\repos\Aoc2021\Aoc\Data\Day{day}Data.txt");
        }
    }
}
