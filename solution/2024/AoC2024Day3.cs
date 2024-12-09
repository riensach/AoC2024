using System.Text.RegularExpressions;
using System.Text;

using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Reflection;
using System.Collections.Generic;

namespace AoC2024.solution
{
    public class AoCDay3
    {

        public AoCDay3(int selectedPart, string input)
        {
           

            int totalValue = 0;

            string pattern = @"mul\(([0-9]{1,3})[,]([0-9]{1,3})[\)]";
            Regex rg = new Regex(pattern);
            MatchCollection matchedWords = rg.Matches(input);
            for (int i = 0; i < matchedWords.Count; i++)
            {

                totalValue = totalValue + (int.Parse(matchedWords[i].Groups[1].Value) * int.Parse(matchedWords[i].Groups[2].Value));
                
                

                //Console.WriteLine("Adding count from iteration " + i);
               

            }

            Console.WriteLine("Total Value Part A: " + totalValue);



            totalValue = 0;
            bool enabled = true;
            pattern = @"(do\(\)|don't\(\))|((mul)\(([0-9]{1,3})[,]([0-9]{1,3})[\)])";
            Regex rg2 = new Regex(pattern);
            MatchCollection matchedWords2 = rg2.Matches(input);
            for (int i = 0; i < matchedWords2.Count; i++)
            {
                if (matchedWords2[i].Groups[1].Value == "do()")
                {
                    enabled = true;
                }
                else if (matchedWords2[i].Groups[1].Value == "don't()")
                {
                    enabled = false;
                }
                else if (enabled == true)
                {
                    totalValue = totalValue + (int.Parse(matchedWords2[i].Groups[4].Value) * int.Parse(matchedWords2[i].Groups[5].Value));
                }


                //Console.WriteLine("Adding count from iteration " + i);


            }

            Console.WriteLine("Total Value Part B: " + totalValue);

            // 28716464 too low





        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public string output;
    }
}