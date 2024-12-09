// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using AdventofCode2024;

namespace AdventofCode2024
{

    class Launch
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Advent of Code 2024 in C# \r");
            Console.WriteLine("------------------------\n");


            int daySelection = 5;
            int yearSelection = 2024;
            int partSelection = 1;

            DaySelection Advent = new DaySelection(daySelection, partSelection, yearSelection);
            
            return;
        }
    }
}
