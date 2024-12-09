using System;
using System.IO;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using System.Data.Common;
using System.Reflection;
using System.Collections.Generic;

namespace AoC2024.solution
{
    public class AoCDay4
    {
        public string[,] grid;
        public string[,] gridTemp;
        int arrayLength = 0;
        int arrayWidth = 0;
        Dictionary<string, string> reflectionLines = new Dictionary<string, string>();

        public AoCDay4(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            List<char> navigationList = new List<char>();
            List<string> startingNodeList = new List<string>();
            arrayLength = lines.Count();
            arrayWidth = lines[0].Length;
            grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);

            string outputGrid = printGrid(arrayLength, arrayWidth);
            //output += outputGrid;

            int row = 0;
            int column = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = character.ToString();
                    var startingLocation = new Position(row, column);
                    var explorerInfo = new Explorer(character.ToString(), startingLocation);
                    string temp = row+","+column;
                    //Console.WriteLine(character.ToString());
                    reflectionLines.Add(temp, character.ToString());
                    column++;
                }
                row++;
                column = 0;
            }

            foreach (KeyValuePair<string, string> kvp in reflectionLines)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            row = 0;
            column = 0;
            int foundXMas = 0;
            int foundXMasSecond = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    if(character.ToString() == "X") {
                        // Start the search!
                        foundXMas = foundXMas + findString(row, column, grid);
                        //Console.WriteLine("Looking from: " + row + " - " + column + "\n");
                    }
                    if (character.ToString() == "A")
                    {
                        // Start the search!
                        foundXMasSecond = foundXMasSecond + findStringSecond(row, column, grid);
                        //Console.WriteLine("Looking from: " + row + " - " + column + "\n");
                    }
                    column++;
                }
                row++;
                column = 0;
            }

            Console.WriteLine("Found xmas part a: " + foundXMas);
            Console.WriteLine("Found xmas part B: " + foundXMasSecond);

            //outputGrid = printGrid(arrayLength, arrayWidth);
            //output += outputGrid;
        }

        public string output;


        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y];
                    //System.Console.Write(toWrite);

                    output += "" + toWrite;
                }
                //System.Console.Write("\n");
                output += "\n";
            }

            return output;
        }

        public int findString(int x, int y, string[,] grid)
        {
            string mIndex = x + "," + y;
            string aIndex = x + "," + y;
            string sIndex = x + "," + y;
            var mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            var aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            var sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            int foundXmasCount = 0;


            mIndex = (x - 1) + "," + y;
            aIndex = (x - 2) + "," + y;
            sIndex = (x - 3) + "," + y;
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex]:".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] :".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] :".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x - 1) + "," + (y - 1);
            aIndex = (x - 2) + "," + (y - 2);
            sIndex = (x - 3) + "," + (y - 3);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x - 1) + "," + (y + 1);
            aIndex = (x - 2) + "," + (y + 2);
            sIndex = (x - 3) + "," + (y + 3);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x + 1) + "," + y;
            aIndex = (x + 2) + "," + y;
            sIndex = (x + 3) + "," + y;
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x + 1) + "," + (y + 1);
            aIndex = (x + 2) + "," + (y + 2);
            sIndex = (x + 3) + "," + (y + 3);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x + 1) + "," + (y - 1);
            aIndex = (x + 2) + "," + (y - 2);
            sIndex = (x + 3) + "," + (y - 3);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }

            mIndex = x + "," + (y - 1);
            aIndex = x + "," + (y - 2);
            sIndex = x + "," + (y - 3);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }            
            
            mIndex = x + "," + (y + 1);
            aIndex = x + "," + (y + 2);
            sIndex = x + "," + (y + 3);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            if (mItem == "M" && aItem == "A" && sItem == "S")
            {
                foundXmasCount++;
            }
            

            //Find above
            //Find below
            //Find left
            //Find right
            //Find diagonal left up
            //Find diagonal left down
            //Find diagonal right up
            //Find diagonal right down

            return foundXmasCount++;
        }



        public int findStringSecond(int x, int y, string[,] grid)
        {
            string mIndex = x + "," + y;
            string mIndex2 = x + "," + y;
            string sIndex = x + "," + y;
            string sIndex2 = x + "," + y;
            var mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            var mItem2 = (reflectionLines.ContainsKey(mIndex2)) ? reflectionLines[mIndex2] : ".";
            var sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            var sItem2 = (reflectionLines.ContainsKey(sIndex2)) ? reflectionLines[sIndex2] : ".";
            int foundXmasCount = 0;


            mIndex = (x - 1) + "," + (y - 1);
            mIndex2 = (x - 1) + "," + (y + 1);
            sIndex = (x + 1) + "," + (y - 1);
            sIndex2 = (x + 1) + "," + (y + 1);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            mItem2 = (reflectionLines.ContainsKey(mIndex2)) ? reflectionLines[mIndex2] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            sItem2 = (reflectionLines.ContainsKey(sIndex2)) ? reflectionLines[sIndex2] : ".";
            if (mItem == "M" && mItem2 == "M" && sItem == "S" && sItem2 == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x - 1) + "," + (y - 1);
            mIndex2 = (x + 1) + "," + (y - 1);
            sIndex = (x - 1) + "," + (y + 1);
            sIndex2 = (x + 1) + "," + (y + 1);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            mItem2 = (reflectionLines.ContainsKey(mIndex2)) ? reflectionLines[mIndex2] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            sItem2 = (reflectionLines.ContainsKey(sIndex2)) ? reflectionLines[sIndex2] : ".";
            if (mItem == "M" && mItem2 == "M" && sItem == "S" && sItem2 == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x + 1) + "," + (y - 1);
            mIndex2 = (x + 1) + "," + (y + 1);
            sIndex = (x - 1) + "," + (y - 1);
            sIndex2 = (x - 1) + "," + (y + 1);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            mItem2 = (reflectionLines.ContainsKey(mIndex2)) ? reflectionLines[mIndex2] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            sItem2 = (reflectionLines.ContainsKey(sIndex2)) ? reflectionLines[sIndex2] : ".";
            if (mItem == "M" && mItem2 == "M" && sItem == "S" && sItem2 == "S")
            {
                foundXmasCount++;
            }

            mIndex = (x + 1) + "," + (y + 1);
            mIndex2 = (x - 1) + "," + (y + 1);
            sIndex = (x + 1) + "," + (y - 1);
            sIndex2 = (x - 1) + "," + (y - 1);
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            mItem2 = (reflectionLines.ContainsKey(mIndex2)) ? reflectionLines[mIndex2] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
            sItem2 = (reflectionLines.ContainsKey(sIndex2)) ? reflectionLines[sIndex2] : ".";
            if (mItem == "M" && mItem2 == "M" && sItem == "S" && sItem2 == "S")
            {
                foundXmasCount++;
            }

            return foundXmasCount++;
        }

        public string[,] createGrid(string[,] grid, int xSize, int ySize)
        {

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = ".";
                }
            }

            return grid;
        }

        public record Position(int x, int y);

        public record Explorer(string text, Position position);
    }
}