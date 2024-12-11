using System;
using System.IO;
using System.Diagnostics;
using System.Data.Common;
using static AoC2024.solution.AoCDay8;
using System.ComponentModel;
using System.Collections.Generic;

namespace AoC2024.solution
{
    public class AoCDay9
    {

        public AoCDay9(int selectedPart, string input)
        {
            string newString = "";
            string tempString = "";
            for (int i = 0; i < input.Count(); i++)
            {
                int blockSize = int.Parse(input[i].ToString());
                //Console.WriteLine(blockSize);
                int id = i;
                if (i % 2 == 0)
                {
                    // size of file
                    tempString = ""; 
                    if(i > 0)
                    {
                        id = (i / 2);
                    }
                    for (int x = 0; x < blockSize; x++)
                    {
                        tempString = tempString + id;
                    }
                        
                    newString = newString + tempString;
                } else
                {
                    // free spacetempString = "";
                    tempString = "";
                    for (int x = 0; x < blockSize; x++)
                    {
                        tempString = tempString + ".";
                    }
                    newString = newString + tempString;
                }
            }
            Console.WriteLine(newString);
            //string updatedString = "";
            tempString = "";
            int getIndex = 0;
            System.Text.StringBuilder updatedString = new System.Text.StringBuilder(newString);


            for (int i = 0; i < updatedString.Length; i++)
            {
                if (updatedString[i].ToString() == "." && i != (updatedString.Length-1))
                {
                    getIndex = updatedString.Length - 1;
                    tempString = updatedString[getIndex].ToString();
                    updatedString.Remove(updatedString.Length - 1,1);
                    while (tempString == ".")
                    {
                        getIndex = updatedString.Length - 1;
                        tempString = updatedString[getIndex].ToString();
                        updatedString.Remove(updatedString.Length - 1, 1);
                    }
                    updatedString[i] = Convert.ToChar(tempString);
                }
            }
            int gapCount = 0;
            for (int i = 0; i < updatedString.Length; i++)
            {
                if (updatedString[i].ToString() == ".")
                {
                    gapCount++;
                }
            }

            Console.WriteLine("Count of gaps: " + gapCount);


            Console.WriteLine(updatedString);

            long checksum = 0;
            for (int i = 0; i < updatedString.Length; i++)
            {
                if(updatedString[i].ToString() != ".")
                {
                    checksum = checksum + (i * long.Parse(updatedString[i].ToString()));
                }
            }
            Console.WriteLine("Part A: " + checksum);

        }

        // too low 799772458

        // too low 90994085674

        public string output;
    }
}