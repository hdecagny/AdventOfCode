﻿// See https://aka.ms/new-console-template for more information

using AdventOfCode2019;

Console.WriteLine($"Which day do you want to solve?");
var day = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

Console.WriteLine($"Which exercise do you want to solve? (1-2)");
var exercise = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());


var classType = Type.GetType($"AdventOfCode2019.Day{day}Solver");
var solver = (SolverBase2019)Activator.CreateInstance(classType);

var answer = exercise == 1 ? solver.SolvePuzzle1() : solver.SolvePuzzle2();

Console.WriteLine(answer);