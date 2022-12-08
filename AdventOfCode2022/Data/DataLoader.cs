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
            return System.IO.File.ReadAllLines(@$"D:\Repos\AdventOfCode2022 - C#\AdventOfCode2022\Data\Day{day}Data.txt");
        }

        public static string LoadDataFromDay(int day)
        {
            return System.IO.File.ReadAllText(@$"D:\Repos\AdventOfCode2022 - C#\AdventOfCode2022\Data\Day{day}Data.txt");
        }
    }
}