using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode.Data;

namespace AdventOfCode.Day5
{
    public class Day5Solver
    {
        public static long SolvePuzzle1()
        {
            var data = DataLoader.LoadDataPerLineFromDay(5)
                .Select(s=>Regex.Split(s,@"\D+" ))
                .Select(s=>s.Select(int.Parse).ToList())
                .ToList();

            var mapStream = new int[1000, 1000];

            foreach (var line in data)
            {
                if (line[0] == line[2])
                {
                    var min = Math.Min(line[1], line[3]);
                    var max = Math.Max(line[1], line[3]);

                    for (var i = min; i <= max; i++)
                    {
                        mapStream[line[0], i] += 1;
                    }
                }

                if (line[1] == line[3])
                {
                    var min = Math.Min(line[0], line[2]);
                    var max = Math.Max(line[0], line[2]);

                    for (var i = min; i <= max; i++)
                    {
                        mapStream[i, line[1]] += 1;
                    }
                }
            }

            return mapStream.Cast<int>().Count(i => i > 1);
        }

        public static long SolvePuzzle2()
        {
            var data = DataLoader.LoadDataPerLineFromDay(5)
                .Select(s => Regex.Split(s, @"\D+"))
                .Select(s => s.Select(int.Parse).ToList())
                .ToList();

            var mapStream = new int[1000, 1000];

            foreach (var line in data)
            {
                if (line[0] == line[2])
                {
                    var min = Math.Min(line[1], line[3]);
                    var max = Math.Max(line[1], line[3]);

                    for (var i = min; i <= max; i++)
                    {
                        mapStream[line[0], i] += 1;
                    }
                }

                else if (line[1] == line[3])
                {
                    var min = Math.Min(line[0], line[2]);
                    var max = Math.Max(line[0], line[2]);

                    for (var i = min; i <= max; i++)
                    {
                        mapStream[i, line[1]] += 1;
                    }
                }

                else
                {
                    var length = Math.Abs(line[2] - line[0]);

                    var lengthDirection = line[2] - line[0] > 0 ? 1 : -1;
                    var widthDirection = line[3] - line[1] > 0 ? 1 : -1;


                    for (var i = 0; i <= length; i++)
                    {
                        mapStream[line[0] + i*lengthDirection, line[1] + i*widthDirection] += 1;
                    }
                }
            }

            return mapStream.Cast<int>().Count(i => i > 1);
        }
    }
}
