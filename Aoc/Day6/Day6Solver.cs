using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace AdventOfCode.Day6
{
    public class Day6Solver
    {
        public static long SolvePuzzle1()
        {
            var data = Regex
                .Split(DataLoader.LoadDataFromDay(6), @"\D+")
                .Select(int.Parse)
                .ToList();

            for (var day = 0; day < 80; day++)
            {
                var newfishes = data.Count(i => i == 0);

                for (var i = 0; i < data.Count; i++)
                {
                    data[i] = data[i] == 0 ? 6 : (data[i] - 1);
                }

                for (var i = 0; i < newfishes; i++)
                {
                    data.Add(8);
                }
            }

            return data.Count;
        }

        public static long SolvePuzzle2()
        {
            var data = Regex
                .Split(DataLoader.LoadDataFromDay(6), @"\D+")
                .Select(int.Parse)
                .ToList();

            var fishperDay = new Dictionary<int, long>();


            for (var lifespawn = 0; lifespawn <= 8; lifespawn++)
            {
                fishperDay.Add(lifespawn, data.Count(s=>s==lifespawn));
            }

            for (var day = 0; day < 256; day++)
            {
                var nextday = new Dictionary<int, long>();

                nextday.Add(0, fishperDay[1]);
                nextday.Add(1, fishperDay[2]);
                nextday.Add(2, fishperDay[3]);
                nextday.Add(3, fishperDay[4]);
                nextday.Add(4, fishperDay[5]);
                nextday.Add(5, fishperDay[6]);
                nextday.Add(6, fishperDay[7]+fishperDay[0]);
                nextday.Add(7, fishperDay[8]);
                nextday.Add(8, fishperDay[0]);

                fishperDay = nextday;
            }

            var answer = fishperDay.Sum(d => d.Value);

            return answer;

        }
    }
}
