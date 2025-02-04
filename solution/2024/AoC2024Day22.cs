﻿using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using static System.Collections.Specialized.BitVector32;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace AoC2024.solution
{
    public class AoCDay22
    {
        public string[,] grid;
        public IDictionary<string, string> cubeTransitions = new Dictionary<string, string>();
        public IDictionary<int, string> cubes = new Dictionary<int, string>();
        public AoCDay22(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Count();
            //Console.WriteLine(arrayLength + "," + arrayWidth);
            grid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);
            int iteratorX = 0;
            int iteratorY = 0;
            int startingX = 0;
            int startingY = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    if(character.ToString()==" ")
                    {
                        grid[iteratorX, iteratorY] = "X";
                    } else
                    {
                        grid[iteratorX, iteratorY] = character.ToString();
                        if (iteratorX == 0 && startingY == 0)
                        {
                            startingY = iteratorY;
                        }
                    }

                    
                    iteratorY++;
                }

                iteratorX++;
                iteratorY = 0;
            }


            //string path = "10R5L5R10L4R5L5";
            string path = "20L34L8L18L5L34R44L22L38L41R25R1L37R34R33R28L29R14L1L36L28R42R18R31L36R38R43L16R6L41R4L41R50R15R10R3L22R20L17R28L4L50R14R13L16L20R3R35L7R13R2L22L24R9R9R12L36R13R31L38L28L50L38L17R46R13L46L1L38L21L35R15L31L5R19L7R18R38R9L15L31R40L16L28R42L3R40R37L1R49L32L5R5L16L34R22R36L3L36R10L50R30L5L13L13L39L42R20R10R27L33L34L22L45L23R47L12L27R36L27L7R13L50R19R3L6R36R45R23L1R19L33R27R28L42L42R48L12L46L17L37L1L40L23R1R49R35L34L43R13R37R31L3L27R30L38R16L49R47L50R19L23R14L45R10L22L17R30L1L28R42L8R50R30R4R45R32L7L18R27R32L12L9R41R25R41R50L4L50L32R13R46L43R3R36R17R19R34R15L46R27L2L39R26L46L42R1L28R28R28R11R26R30L49L5L33L32R9R26R28L15L2R14L29R27R28L38L5R27L18R14L33R11L50R6L22L16R36R7R10R9L24R27L46L16R35L46R9L46R32R35L39L36R41R23L30L41L31L50L6L45R27R18L17R13R35L46R18R7R6L12L46L37R3L45L18R44L48L17L11L6R12L44R46L17L39R31L30L19L49R47L18R21L46R3R24L31L26L38R28R2L27R49R23R49R35L36R15L45R20R17R42R39R19R3R10R29L44L23L35L16L44R13R21L47L44R34R4L29R25L31R33L8L5R32R47L13L30R34R31R43L43R28R47L41L20L3R34L1R13R29L10R7L32L30R12L35L27L32L25L19L1L10L30R3L43R6R35R44R25L31L33R34L32R48R19R15R20R43L7R27R33L27L37R12L34R24L13R2L14R15L27L11R35R23R31R3R8R16R22R28L36L26R10R28L3R26R49L32R19L13R20L36R3R19R22R33L26R25R27L15R17L44L37L28L37R14R35R34R27L26L27L15L21R45L3R3R17L25L5L48L28R38L36L27L25R8R20R42R18L35R26L4L41L35L47L28L12R25R5L25L30R16R42R18R6R17R33R21L25L31R14L31R46L31L30R9R11R48L28R48L26R20L30R32L42L30R6L39L2L10L14L7R13L27R19R32R26L16R47L6L41L41R12R7R3L17R36L11R49L48R33R14L26L48L14R33R48L38R24L12L27R8L47R24R18R30L29R9R24R9L27L38R16L49R48L16L16L9R8L50R39L46R21L40L7L7L25R8R42L29R36R30L4L32L19R11R12R1L32R47L13L32R14R5L29L3R14R30L5R7L42L14L34R40L8R10L41L19R4R37R13R46R34R34L14L17L8R35L45L7L11R44R9L6L35L20L39L23R23L29L21R48R20R8L41R32R28L3L18L32R26L49L7L29L4R7L39L35R44R38L5R14R10R13L10R38L3L1R3R29L11L50R39L31L48L8L49R40R27R11R36R10R6L9L9R2L33L28L10L19R17L10R46R46R46L9L7L18R25R9L50R35R45L40L13L46L27L32L2L13R33L22R16L48L11L14L17R19L22R28R20R1R43R7R27R12R25L18L40L36R21L7R24L2L44L24L41L16L22L23L46L19L27L22L14R39L35R8L45R29L20R21R43L10R43R44L7L47L28R10R30L11R8R22R35R6L17L34L47L5L47L42R33L37L36L46L10R14R42R48L29L43R6L33L23R37R40R42L11R14L48R6L12L33L47R26R26R19R7L7L18L37L19L26R41L14L25L4R48R32R17L43L1R28L2L8L20L9R9R47L4R15L5R1L41L12L50L27R1R49L3L33R15L28L15R8L5R41L20L13R47R48L29R2R20L48R33R17L24L28L46R49R27L22R8L7R6R47R50L25R11L46R12L29R36L26L47L47R9L30R24L44L19R26R10R43R21L30R9R30R21L19R3L26L46L12R42L6R50R5R37R25R14L20L43R19R42L22R1L27L44R10R13L29L2R31L42R16L22R35L12R8L38L41L22L42L16R20R9L12L17L8L1L18L33R18L5L24L9R17R31R7L23L38L30R35L5L19R12R38R49L48L4R47R32R18L49L10R6R4R37R1L47L40L40R21L40L20R17R20R20L1R25R49R16R19L3L44L17R3R24R41R16R18R42R44R7L20R43L20R34L22R45L50R42L8R34R33L41R40R28L4L40L2R25L43L40L35L17L2R23R20R29L32R22R28L4R18L36R19R14R24R18L18R31R39L43L26L25R1R32R47L6R27R46R36R16L4R17L43L2R28R40L19L10L18L13R14L31L50L43L46R11L41L39R27L11R15R17L1R15R48L14L39L32L35L10R31L25L33L42R35R38R31L11R5R38R35R3R2R35R27R4R19L28R18R14L39R27L23R9R39R22R14R20R44L11L26L16R6R31L28R13L4R11R48R14L1R36L2R4L5L8R12R10L30R22L5R32L22R18R41R8R10R4L12L5L16L44L17L15L14R40L26L25R29L43L39R27L16R2R31R27L37L6L9R34R47R43L45L8R16L49L36L18R23R38L42R50L8R13L23L11R50L18L40R26L22L30L44L16R35R4L44L38L34R36L7R21L23R26R24L42L30L8L30L1L23L18R22R26L47L28L6R4R21L46R18L37R33L40L8L27L11R30R14R33L40R15L25L35R23R49L34R43R9R10L20R28L37L17R29L22L32R39L22L38L22L27L16L28L21R29L8R46R28R50L11L20R8R28R26R21R10R46L50L18L12L19L28R40R47R48R2R38R34R3R45L23L35R10R25R18L38L12L7R14L19R47L9R12R6R16L41R24L50L41R18R23L46R26L45L27L12R9R9R32R15R26L47R13R35R28R21L26L25R5L7L17L5L3R14R43R48L9L4R29L17R38R4L20L21R46L2R15L12R19L27R41R8L3R39R50L5R46R15L41R17L26L21R22L38L3L48L35L22R35R5R8L25R28L11L7R17L25R43L19L49R32L31L6R9L34L26L11R21L22R31L3R14L2R10L43L14L1L41R33R43R32R45R28L10L6L1L3L3L7L18L3L16R6L1L13R30R43L28R34L44L34L30L47L19L2R25R4R40R42L49R1R16R31L20L27L34L1L35R47L33L18L31L12L21R36R36R45L21R13L9L43R27R10R11R6L24R20R17R28L24L6R23L34R3R42R48L50L46L17R25R11L45R8L12R5L33R1L33R48R43R33R20L39R33L32L14L35R27L26L12L25L19R10L44L24L13L12R24R43R6L43L46R24R25L9L37R11L28L18L33L10L16R11R3R5R19R48L42L36R47R32L36L6L32L19L21R49R3L38R33R19L27R15L10R11L32L4L23R38L11L21R16L47R21L42L12L25L23L42L41L4L8R13R20L47L47R25R1R17L33R48L17R26L17R49L10L41L43R9L29L3R42L35R26L12L17L45L42R10R31R22L46L35R35R48R5R29L9L38R44L28R44R39R19L38L49R34R12R38R45R17R18L20R26L28L47L5L24R11R21R43R34L20R39L41L11L20R10L23R21R37L4R40L42R36L32L48L35R35R29L9L10L3R39L49R17L17R42R2R9R11L17L32R20R3L16L30L49L28L22L7L4R31L31L8L48L49R4L30R7L12R38L29L12R46R14L13L32R39L11L49R48R20R6R25L9L46R20R6L24R14L37R29L18L43R28L30L7R12R19L16L27L3L21L40L24L9R42R22L31L34L5L9R37L49L1R42L16L17L37R16R15L48L22R46L46L43L25R2R13R17L31R24L42L8R20L15L13L35R12L11L10L42R4R32R27L48R48L16L15R31L2L26R20R31R34R11R43R4R16L45L44R20L16R16L12L6R43L31R9L49L31R44L47L9L13R29R11L8R28L47R23L5L19R49L21R9L3R39R44R13L2L37R12R5R28R48R46L12L45L8R28R33R16R26L25L10L10R17L33R40R26R20R44R38R41L35L16R45R31L18L35R3R6L5R22L18L29L1R5L14R17R7L15R6L44R29L7R30L17R15R4R7L15R33R21L41L18R32L19L9L49L2R40L38L44L43L47L1L24L13L11L11L31R43L42L13R17L31L26R13R27R18R31R21L43L38R13R41R1R5R14L28R39L45L16L16L8L25R13L50R7L29R26L23L11R8L23R41R41L19R39R16R45L17L8R4R43L49R18R41L16L10R29L50R14R16R11L36R18R45L49R31L30L8L35L4L2R12L42L31R13L49R43R35R44L41R25R9R1L4L22L42L39L45R14L17L20R17L40L21L29R9L46R7L18R1L49L24L12L29R41R11R28R25R4R33L29R16R31R5L33L48R1R32L47R1L40R6R40L15L18R11R4R48R31R28R15L33L17R13R12L11L23L42R40L38L45L38L18L20L16R27R31R25L45R34L21R33L6R26L19L36R2L13R49L37R4L38R27L3R1R14L2L9L13R25L39L15R1L2R33L19R3R32R18L29L37R15L21L41L10R34R42R25R10R17L15R29L37L39R35L44L14L2L30L19L38L17R44L34R36R22R1R5R50L23L16L23L43L14L10R10L29L10R36L12R32L21R2R4R31R20L21L44R19R12L38L20R14R15R5";

            /*cubes.Add(1, "0,4,3,7");
            cubes.Add(2, "4,4,7,7");
            cubes.Add(3, "8,4,11,7");
            cubes.Add(4, "0,8,3,11");
            cubes.Add(5, "8,0,11,3");
            cubes.Add(6, "12,0,15,3");

            // 6032/5031
            

            grid[0, 50] = "A";
            grid[49, 99] = "A";
            grid[50, 50] = "B";
            grid[99, 99] = "B";
            grid[100, 50] = "C";
            grid[149, 99] = "C";
            grid[0, 100] = "D";
            grid[49, 149] = "D";
            grid[100, 0] = "E";
            grid[149, 49] = "E";
            grid[150, 0] = "F";
            grid[199, 49] = "F";
            Console.WriteLine(grid[0, 50]);
            Console.WriteLine(grid[99, 99]);
            Console.WriteLine(grid[149, 99]);
            output += printGrid(arrayLength, arrayWidth);

            return;*/


            cubes.Add(1,"0,50,49,99");
            cubes.Add(2,"50,50,99,99");
            cubes.Add(3,"100,50,149,99");
            cubes.Add(4,"0,100,49,149");
            cubes.Add(5,"100,0,149,49");
            cubes.Add(6,"150,0,199,49");

            // Format: name: <cubeFrom>-<direction> value <cubeTo>-<direction>
            cubeTransitions.Add("1-U","6-R");
            cubeTransitions.Add("1-L","5-R");
            cubeTransitions.Add("2-L","5-D");
            cubeTransitions.Add("2-R","4-U");
            cubeTransitions.Add("3-D","6-L");
            cubeTransitions.Add("3-R","4-L");
            cubeTransitions.Add("4-U","6-U");
            cubeTransitions.Add("4-R","3-L");
            cubeTransitions.Add("4-D","2-L");
            cubeTransitions.Add("5-U","2-R");
            cubeTransitions.Add("5-L","1-R");
            cubeTransitions.Add("6-L","1-D");
            cubeTransitions.Add("6-D","4-D");
            cubeTransitions.Add("6-R","3-U");

            Console.WriteLine(startingX + "," + startingY);


            int currentX = startingX;
            int currentY = startingY;

            string pattern = @"([0-9]{1,2})|([L,R])";
            Regex regex = new Regex(pattern);
            string[] substrings = regex.Split(path);
            string facing = "R";
            foreach (string line in substrings)
            {
                if(line=="")
                {
                    continue;
                }
                //Console.WriteLine(line);
                if(line == "R")
                {
                    if(facing == "R")
                    {
                        facing = "D";
                    } else if (facing == "D")
                    {
                        facing = "L";
                    }
                    else if (facing == "L")
                    {
                        facing = "U";
                    }
                    else if (facing == "U")
                    {
                        facing = "R";
                    }
                } else if (line == "L")
                {
                    if (facing == "R")
                    {
                        facing = "U";
                    }
                    else if (facing == "D")
                    {
                        facing = "R";
                    }
                    else if (facing == "L")
                    {
                        facing = "D";
                    }
                    else if (facing == "U")
                    {
                        facing = "L";
                    }
                } else
                {
                    // It's a number!
                    if (facing == "R")
                    {
                        // Move right by the number
                        for (int i = 0; i < int.Parse(line); i++)
                        {
                            bool validCoord = true;
                            string gridValue = "";
                            if (currentY + 1 >= arrayWidth) {
                                validCoord = false;
                            } else
                            {
                                gridValue = grid[currentX, currentY + 1];
                            }

                            if (gridValue != "#" && gridValue != "X" && validCoord)
                            {
                                // We can move here
                                //grid[currentX, currentY] = ">";
                                currentY = currentY + 1;
                                //Console.WriteLine("Moving along one space.");
                            } else if (!validCoord || gridValue != "#")
                            {
                                // We need to do a wrap AROUND DA CUBE
                                //Console.WriteLine("Attempting to wrap.");
                                bool hitWall = false;
                                for (int y = 0; y < arrayWidth; y++)
                                { 
                                    if(grid[currentX, y] == "X")
                                    {
                                        // Haven't found a valid place yet
                                        continue;
                                    } else if (grid[currentX, y] == "#")
                                    {
                                        // Hit a wall when wrapping, so stopping
                                        //Console.WriteLine("We can't move any further, so breaking.");
                                        hitWall = true;
                                        break;
                                    } else
                                    {
                                        // Found a suitable landing spot
                                        //grid[currentX, currentY] = ">";
                                        currentY = y;
                                        break;
                                    }
                                }

                                if (hitWall)
                                {
                                    break;
                                }
                            } else
                            {
                                // We can't move, so break from this movement cycle
                                //Console.WriteLine("We can't move any further, so breaking.");
                                break;
                            }
                        }
                    }
                    else if (facing == "D")
                    {
                        // Move down by the number
                        for (int i = 0; i < int.Parse(line); i++)
                        {
                            bool validCoord = true;
                            string gridValue = "";
                            if (currentX + 1 >= arrayLength)
                            {
                                validCoord = false;
                            }
                            else
                            {
                                gridValue = grid[currentX + 1, currentY];
                            }

                            if (gridValue != "#" && gridValue != "X" && validCoord)
                            {
                                // We can move here
                                //grid[currentX, currentY] = "v";
                                currentX = currentX + 1;
                                //Console.WriteLine("Moving along one space.");
                            }
                            else if (!validCoord || gridValue != "#")
                            {
                                // We need to do a wrap AROUND DA CUBE
                                //Console.WriteLine("Attempting to wrap.");
                                bool hitWall = false;
                                for (int x = 0; x < arrayLength; x++)
                                {
                                    if (grid[x, currentY] == "X")
                                    {
                                        // Haven't found a valid place yet
                                        continue;
                                    }
                                    else if (grid[x, currentY] == "#")
                                    {
                                        // Hit a wall when wrapping, so stopping
                                        //Console.WriteLine("We can't move any further, so breaking.");
                                        hitWall = true;
                                        break;
                                    }
                                    else
                                    {
                                        // Found a suitable landing spot
                                        //grid[currentX, currentY] = "v";
                                        currentX = x;
                                        break;
                                    }
                                }

                                if (hitWall)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                // We can't move, so break from this movement cycle
                                //Console.WriteLine("We can't move any further, so breaking.");
                                break;
                            }
                        }
                    }
                    else if (facing == "L")
                    {
                        
                        // Move left by the number
                        for (int i = 0; i < int.Parse(line); i++)
                        {
                            bool validCoord = true;
                            string gridValue = "";
                            if (currentY - 1 < 0)
                            {
                                validCoord = false;
                            }
                            else
                            {
                                gridValue = grid[currentX, currentY - 1];
                            }

                            if (gridValue != "#" && gridValue != "X" && validCoord)
                            {
                                // We can move here
                                //grid[currentX, currentY] = "<";
                                currentY = currentY - 1;
                                //Console.WriteLine("Moving along one space.");
                            }
                            else if (!validCoord || gridValue != "#")
                            {
                                // We need to do a wrap AROUND DA CUBE
                                //Console.WriteLine("Attempting to wrap.");
                                bool hitWall = false;
                                for (int y = arrayWidth - 1; y > -1; y--)
                                {
                                    if (grid[currentX, y] == "X")
                                    {
                                        // Haven't found a valid place yet
                                        continue;
                                    }
                                    else if (grid[currentX, y] == "#")
                                    {
                                        // Hit a wall when wrapping, so stopping
                                        //Console.WriteLine("We can't move any further, so breaking.");
                                        hitWall = true;
                                        break;
                                    }
                                    else
                                    {
                                        // Found a suitable landing spot
                                        //grid[currentX, currentY] = "<";
                                        currentY = y;
                                        break;
                                    }
                                }
                                if(hitWall)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                // We can't move, so break from this movement cycle
                                //Console.WriteLine("We can't move any further, so breaking.");
                                break;
                            }
                        }
                    }
                    else if (facing == "U")
                    {
                        // Move up by the number
                        for (int i = 0; i < int.Parse(line); i++)
                        {
                            bool validCoord = true;
                            string gridValue = "";
                            if (currentX - 1 < 0)
                            {
                                validCoord = false;
                            }
                            else
                            {
                                gridValue = grid[currentX - 1, currentY];
                            }

                            if (gridValue != "#" && gridValue != "X" && validCoord)
                            {
                                // We can move here
                                //grid[currentX, currentY] = "^";
                                currentX = currentX - 1;
                                //Console.WriteLine("Moving along one space.");
                            }
                            else if (!validCoord || gridValue != "#")
                            {
                                // We need to do a wrap AROUND DA CUBE
                                //Console.WriteLine("Attempting to wrap.");
                                bool hitWall = false;
                                for (int x = arrayLength - 1; x > -1; x--)
                                {
                                    if (grid[x, currentY] == "X")
                                    {
                                        // Haven't found a valid place yet
                                        continue;
                                    }
                                    else if (grid[x, currentY] == "#")
                                    {
                                        // Hit a wall when wrapping, so stopping
                                        //Console.WriteLine("We can't move any further, so breaking.");
                                        hitWall = true;
                                        break;
                                    }
                                    else
                                    {
                                        // Found a suitable landing spot
                                        //grid[currentX, currentY] = "^";
                                        currentX = x;
                                        break;
                                    }
                                }

                                if (hitWall)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                // We can't move, so break from this movement cycle
                                //Console.WriteLine("We can't move any further, so breaking.");
                                break;
                            }
                        }
                    }
                }
            }


            
            int calculateScore = 0;
            int facingValue = 0;
            if(facing=="L")
            {
                facingValue = 2;
            } else if (facing == "R")
            {
                facingValue = 0;
            }
            else if (facing == "U")
            {
                facingValue = 3;
            }
            else if (facing == "D")
            {
                facingValue = 1;
            }

            

            calculateScore = 1000 * (currentX+1) + 4 * (currentY+1) + facingValue;

            //output = printGrid(arrayLength, arrayWidth);
            output += "Part A: " + calculateScore;

















            // Need to define "sides" and which "side" to go to and how to orient "x,y" and how directions change etc
            // Maybe this is a string, or some kind of dictionary or list?















            currentX = startingX;
            currentY = startingY;
            Console.WriteLine(currentX + " - " + startingY);

            pattern = @"([0-9]{1,2})|([L,R])";
            regex = new Regex(pattern);
            substrings = regex.Split(path);
            facing = "R";
            foreach (string line in substrings)
            {
                if (line == "")
                {
                    continue;
                }
                //Console.WriteLine(line);
                if (line == "R")
                {
                    if (facing == "R")
                    {
                        facing = "D";
                    }
                    else if (facing == "D")
                    {
                        facing = "L";
                    }
                    else if (facing == "L")
                    {
                        facing = "U";
                    }
                    else if (facing == "U")
                    {
                        facing = "R";
                    }
                }
                else if (line == "L")
                {
                    if (facing == "R")
                    {
                        facing = "U";
                    }
                    else if (facing == "D")
                    {
                        facing = "R";
                    }
                    else if (facing == "L")
                    {
                        facing = "D";
                    }
                    else if (facing == "U")
                    {
                        facing = "L";
                    }
                }
                else
                {
                    // It's a number!
                    
                    // Move right by the number
                    for (int i = 0; i < int.Parse(line); i++)
                    {
                        bool validCoord = true;
                        string gridValue = "";
                        if (currentY + 1 >= arrayWidth && facing == "R")
                        {
                            validCoord = false;
                        }
                        else if(facing == "R")
                        {
                            gridValue = grid[currentX, currentY + 1];
                        }
                        else if (currentX + 1 >= arrayLength && facing == "D")
                        {
                            validCoord = false;
                        }
                        else if(facing == "D")
                        {
                            gridValue = grid[currentX + 1, currentY];
                        } else if (currentY - 1 < 0 && facing == "L")
                        {
                            validCoord = false;
                        }
                        else if (facing == "L")
                        {
                            gridValue = grid[currentX, currentY - 1];
                        } else if (currentX - 1 < 0 && facing == "U")
                        {
                            validCoord = false;
                        }
                        else if (facing == "U")
                        {
                            gridValue = grid[currentX - 1, currentY];
                        }

                        if (gridValue != "#" && gridValue != "X" && validCoord && facing == "R")
                        {
                            // We can move here
                            currentY = currentY + 1;
                        } else if (gridValue != "#" && gridValue != "X" && validCoord && facing == "D")
                        {
                            // We can move here
                            currentX = currentX + 1;
                        }
                        else if (gridValue != "#" && gridValue != "X" && validCoord && facing == "L")
                        {
                            // We can move here
                            currentY = currentY - 1;
                        }
                        else if (gridValue != "#" && gridValue != "X" && validCoord && facing == "U")
                        {
                            // We can move here
                            currentX = currentX - 1;
                        }
                        else if (!validCoord || gridValue != "#" || gridValue == "X")
                        {
                            // We need to do a wrap
                            // First, what cube are we in?
                            int currentCube = 0;

                            int cubeStartingX = 0;
                            int cubeStartingY = 0;
                            int cubeEndingX = 0;
                            int cubeEndingY = 0;

                            foreach (KeyValuePair<int, string> entry in cubes)
                            {
                                var value = entry.Value;
                                var index = entry.Key;
                                string[] cubeCoords = value.Split(',');
                                cubeStartingX = int.Parse(cubeCoords[0]);
                                cubeStartingY = int.Parse(cubeCoords[1]);
                                cubeEndingX = int.Parse(cubeCoords[2]);
                                cubeEndingY = int.Parse(cubeCoords[3]);
                                //Console.WriteLine(currentX +" - " + currentY + " - " + cubeStartingX + " - " + cubeEndingX + " - " + cubeStartingY + " - " + cubeEndingY);
                                if (currentX >= cubeStartingX && currentX <= cubeEndingX && currentY >= cubeStartingY && currentY <= cubeEndingY)
                                {
                                    // Found our cube!
                                    currentCube = index;
                                    break;
                                }
                            }

                            // Next, where do we go and what direction would we change into?
                            string cubeToGoTo = cubeTransitions[currentCube + "-" + facing];
                            string[] cubeToGoToDetails = cubeToGoTo.Split('-');
                            int cubeToGoToInt = int.Parse(cubeToGoToDetails[0]);
                            string cubeToGoToDirection = cubeToGoToDetails[1];

                            // Now we need to figure out how to change the X/Y
                            string[] landingCubeCoords = cubes[cubeToGoToInt].Split(",");

                            int landingCubeStartingX = int.Parse(landingCubeCoords[0]);
                            int landingCubeStartingY = int.Parse(landingCubeCoords[1]);
                            int landingCubeEndingX = int.Parse(landingCubeCoords[2]);
                            int landingCubeEndingY = int.Parse(landingCubeCoords[3]);

                            int newLandingX = 0;
                            int newLandingY = 0;

                            int gapFromTop = currentX - cubeStartingX;
                            int gapFromLeft = currentY - cubeStartingY;

                            //Console.WriteLine("Sending from cube " + currentCube + " to cube " + cubeToGoToInt);

                            //if ((facing == "U" || facing == "D") && cubeToGoToDirection == "R") // 
                            if ((facing == "U") && cubeToGoToDirection == "R") // Y
                            {
                                // Need to invert the X/Y axis

                                newLandingX = landingCubeStartingX + gapFromLeft;
                                newLandingY = landingCubeStartingY;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 1. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            }
                            //else if ((facing == "U" || facing == "D") && cubeToGoToDirection == "L") // 
                            else if ((facing == "D") && cubeToGoToDirection == "L") // Y
                            {
                                // Need to invert the X/Y axis

                                newLandingX = landingCubeStartingX + gapFromLeft;
                                newLandingY = landingCubeEndingY;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 2. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            }
                            //else if ((facing == "L" || facing == "R") && cubeToGoToDirection == "U") // 
                            else if ((facing == "R") && cubeToGoToDirection == "U") // Y
                            {
                                // Need to invert the X/Y axis

                                newLandingX = landingCubeEndingX;
                                newLandingY = landingCubeStartingY + gapFromTop;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 3. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            }
                            //else if ((facing == "L" || facing == "R") && cubeToGoToDirection == "D") // 
                            else if ((facing == "L") && cubeToGoToDirection == "D") // Y
                            {
                                // Need to invert the X/Y axis

                                newLandingX = landingCubeStartingX;
                                newLandingY = landingCubeStartingY + gapFromTop;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 4. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            }
                           // else if ((facing == "L" || facing == "R") && cubeToGoToDirection == "L") // 
                            else if ((facing == "R") && cubeToGoToDirection == "L") // Y
                            {
                                // Straight convert 

                                newLandingX = landingCubeEndingX - gapFromTop;
                                newLandingY = landingCubeEndingY;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 5. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            }
                            //else if ((facing == "L" || facing == "R") && cubeToGoToDirection == "R") // 
                            else if ((facing == "L") && cubeToGoToDirection == "R") // Y
                            {
                                // Straight convert

                                newLandingX = landingCubeEndingX - gapFromTop;
                                newLandingY = landingCubeStartingY;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 6. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            }
                            else if ((facing == "U" || facing == "D") && cubeToGoToDirection == "U") // 
                            {
                                // Straight convert

                                newLandingX = landingCubeEndingX;
                                newLandingY = landingCubeStartingY + gapFromLeft;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 7. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            }
                            else if ((facing == "U" || facing == "D") && cubeToGoToDirection == "D") // 
                            {
                                // Straight convert

                                newLandingX = landingCubeStartingX;
                                newLandingY = landingCubeStartingY + gapFromLeft;
                                //Console.WriteLine("Sending from " + currentX + "," + currentY + " to " + newLandingX + "," + newLandingY + ". Record 8. Gap Left:" + gapFromLeft + ", Top:" + gapFromTop + " :: Facing Old:" + facing + ", facing new:" + cubeToGoToDirection);
                            } else
                            {
                                //Console.WriteLine("WHAT GOES HERE???");
                            }


                            // HERE - figure out if we can land here
                            //Console.WriteLine(grid[newLandingX, newLandingY]);
                            if (grid[newLandingX, newLandingY] == "#")
                            {
                                // Hit a wall, can't do this, so break;
                                //Console.WriteLine("HIT WALL ON WRAP");
                                break;
                            }
                            //Console.WriteLine("Moved");
                            // Otherwise we assume we're legit, so we land
                            currentX = newLandingX;
                            currentY = newLandingY;
                            facing = cubeToGoToDirection;
                         

                        }
                        else
                        {
                            //Console.WriteLine("FOUND THIS");
                            // We can't move, so break from this movement cycle
                            //Console.WriteLine("We can't move any further, so breaking.");
                            break;
                        }
                    }                    
                    
                }
            }

           
            // 159039 = too high
            // 159036 = too high
            // 111335 = wrong
            // 19526 = too low
            // 190184
            // 21308 wrong
            calculateScore = 0;
            facingValue = 0;
            if (facing == "L")
            {
                facingValue = 2;
            }
            else if (facing == "R")
            {
                facingValue = 0;
            }
            else if (facing == "U")
            {
                facingValue = 3;
            }
            else if (facing == "D")
            {
                facingValue = 1;
            }

            // 41467 too low
            // 38563 too low

            calculateScore = 1000 * (currentX + 1) + 4 * (currentY + 1) + facingValue;

            //output = printGrid(arrayLength, arrayWidth);
            output += "\nPart B: " + calculateScore;

            Console.WriteLine(currentX + "," + currentY + "," + facing + " : " + calculateScore);
        }



        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y];
                    output += "" + toWrite;
                }
                output += "\n";
            }
            return output;
        }

        public void createGrid(int xSize, int ySize)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = "X";
                }
            }
        }

        public string output;
    }
}