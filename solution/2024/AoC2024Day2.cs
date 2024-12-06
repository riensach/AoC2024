using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks.Sources;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

namespace AoC2024.solution
{
    public class AoCDay2
    {
        public AoCDay2(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int safeReports = 0;
            int isSafe = 0;
            int isIncreasing = 0;

            foreach (string line in lines)
            {
                string[] games = line.Split(" ");
                isSafe = 0;
                isIncreasing = 0;

                isSafe = checkSafe(games);

                if (isSafe == 0)
                {
                    //Console.WriteLine("FOUND SAFE REPORT line " + line + "\n");
                    safeReports++;
                }

            }



            Console.WriteLine("Safe Reports Part A: " + safeReports + "\n");





            safeReports = 0;
            int foundSafe = 0;

            foreach (string line in lines)
            {
                string[] games = line.Split(" ");
                foundSafe = 0;
                foundSafe = foundSafe + checkSafe(games);
                Console.WriteLine(line + "\n");

                if (foundSafe == 0)
                {
                    // Console.WriteLine("FOUND SAFE REPORT line " + line + " - " + foundSafe + "\n");
                    games.ToList().ForEach(i => Console.Write(i.ToString() + " "));
                    Console.Write("\n");
                    safeReports++;
                    continue;
                } else
                {
                    for (int i = 0; i < games.Length; i++)
                    {
                        var foos = new List<string>(games);
                        foos.RemoveAt(i);
                        string[] gamesNew = foos.ToArray();
                        gamesNew.ToList().ForEach(i => Console.WriteLine(i.ToString() + " "));
                        Console.Write("\n");
                        //Console.WriteLine(gamesNew.ToString() +  " - ") ;
                        foundSafe = foundSafe + checkSafe(gamesNew);
                    }

                    if (foundSafe < games.Length + 1)
                    {
                        //Console.WriteLine("FOUND SAFE REPORT line " + line + " - " + foundSafe + "\n");
                        safeReports++;
                    }
                }

                

            }

            // 838 too high
            // 463 too low
            // 441 too low

            Console.WriteLine("Safe Reports Part B: " + safeReports + "\n");




        }
        public int checkSafe(string[] games)
        {
            int isSafe = 0;
            int isIncreasing = 0;
            for (int i = 0; i < games.Length - 1; i++)
            {
                if (Math.Abs(Int32.Parse(games[i]) - Int32.Parse(games[i + 1])) > 3 || Math.Abs(Int32.Parse(games[i]) - Int32.Parse(games[i + 1])) < 1)
                {
                    // Not safe, so break
                    isSafe = 1;
                    break;
                }
                if (i == 0)
                {
                    if (Int32.Parse(games[i]) > Int32.Parse(games[i + 1]))
                    {
                        isIncreasing = 1;
                    }
                }
                else
                {
                    if ((Int32.Parse(games[i]) > Int32.Parse(games[i + 1]) && isIncreasing == 1) || (Int32.Parse(games[i]) < Int32.Parse(games[i + 1]) && isIncreasing == 0))
                    {

                    }
                    else
                    {
                        // Not safe, so break
                        isSafe = 1;
                        break;
                    }
                }
            }
            return isSafe;

        }

        public string output;
    }
}