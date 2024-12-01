using System;
using System.IO;
using System.Diagnostics;
using System.Security;
using System.Windows.Markup;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Windows.Devices.Power;
using static System.Formats.Asn1.AsnWriter;
using System.Collections.Generic;

namespace AoC2024.solution
{
    public class AoCDay1
    {

        public AoCDay1(int selectedPart, string input)
        {

            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            List<int> calibrationValues = new List<int>();
            List<int> calibrationValuesSecond = new List<int>();

            foreach (string line in lines)
            {
                string[] games = line.Split("   ");

                calibrationValues.Add(Int32.Parse(games[0]));
                calibrationValuesSecond.Add(Int32.Parse(games[1]));

            }

            calibrationValues.Sort();
            calibrationValuesSecond.Sort();

            int totalGap = 0;
            int totalGapSecond = 0;

            for (int i = 0; i < calibrationValues.Count; i++)
            {
                totalGap = totalGap + Math.Abs(calibrationValuesSecond[i] - calibrationValues[i]);
            }

            Console.WriteLine("Part A: " + totalGap);

            var g = calibrationValues.GroupBy(i => i);

            for (int i = 0; i < calibrationValues.Count; i++)
            {
                int foundCount = calibrationValuesSecond.Where(x => x.Equals(calibrationValues[i])).Count();
                totalGapSecond = totalGapSecond + Math.Abs((calibrationValues[i] * foundCount));
            }

            Console.WriteLine("Part B: " + totalGapSecond);

        }

        public string output;


    }
}