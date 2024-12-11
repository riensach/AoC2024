using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Windows.UI.StartScreen;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace AoC2024.solution
{
    public class AoCDay10
    {

        public string[,] grid;
        public string[,] gridTemp;
        int arrayLength = 0;
        int arrayWidth = 0;
        Dictionary<string, string> reflectionLines = new Dictionary<string, string>();


        public AoCDay10(int selectedPart, string input)
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

            int row = 0;
            int column = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = character.ToString();
                    var startingLocation = new Position(row, column);
                    var explorerInfo = new Explorer(character.ToString(), startingLocation);
                    string temp = row + "," + column;
                    //Console.WriteLine(character.ToString());
                    reflectionLines.Add(temp, character.ToString());
                    column++;
                }
                row++;
                column = 0;
            }

            row = 0;
            column = 0;
            int foundXMas = 0;
            int foundXMasSecond = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    if (character.ToString() == "0")
                    {
                        // Start the search!
                        foundXMas = foundXMas + findString(row, column, grid);
                        //Console.WriteLine("Looking from: " + row + " - " + column + "\n");
                    }
                    column++;
                }
                row++;
                column = 0;
            }




        }

        public int findString(int x, int y, string[,] grid)
        {
            int trailScore = 0;
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
            mItem = (reflectionLines.ContainsKey(mIndex)) ? reflectionLines[mIndex] : ".";
            aItem = (reflectionLines.ContainsKey(aIndex)) ? reflectionLines[aIndex] : ".";
            sItem = (reflectionLines.ContainsKey(sIndex)) ? reflectionLines[sIndex] : ".";
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

        public string output;

    }
}