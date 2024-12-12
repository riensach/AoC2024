using System;
using System.IO;
using System.Diagnostics;
using System.Data.Common;
using static AoC2024.solution.AoCDay8;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;

namespace AoC2024.solution
{
    public class AoCDay9
    {

        public AoCDay9(int selectedPart, string input)
        {
            string newString = "";
            string tempString = "";
            IDictionary<int, string> fileList = new Dictionary<int, string>();
            List<string> fileListList = new List<string>();
            for (int i = 0; i < input.Count(); i++)
            {
                long blockSize = long.Parse(input[i].ToString());
                //Console.WriteLine(blockSize);
                long id = i;
                if (i % 2 == 0)
                {
                    // size of file
                    tempString = ""; 
                    if(i > 0)
                    {
                        id = (i / 2);
                    }
                    for (long x = 0; x < blockSize; x++)
                    {
                        tempString = tempString + id;
                        fileListList.Add(id.ToString());
                    }

                    fileList.Add(i, id.ToString());
                    newString = newString + tempString;
                } else
                {
                    // free spacetempString = "";
                    tempString = "";
                    for (long x = 0; x < blockSize; x++)
                    {
                        tempString = tempString + ".";
                        fileListList.Add(".");
                    }
                    newString = newString + tempString;
                    fileList.Add(i, ".");
                }
            }
            //Console.WriteLine(newString);
            foreach (KeyValuePair<int, string> kvp in fileList)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            fileListList.ForEach(i => Console.Write("{0}\n", i));
            //string updatedString = "";
            tempString = "";
            int getIndex = 0;
            System.Text.StringBuilder updatedString = new System.Text.StringBuilder(newString);

            //Console.WriteLine(fileListList.Count);
            for (int i = 0; i < fileListList.Count; i++)
            {
                if (fileListList[i].ToString() == "." && i != (fileListList.Count - 1))
                {
                    getIndex = fileListList.Count - 1;
                    tempString = fileListList[getIndex].ToString();
                    fileListList.RemoveAt(fileListList.Count - 1);
                    while (tempString == ".")
                    {
                        getIndex = fileListList.Count - 1;
                        tempString = fileListList[getIndex].ToString();
                        fileListList.RemoveAt(fileListList.Count - 1);
                    }
                    if(i < fileListList.Count)
                    {
                        fileListList[i] = tempString;
                    } else
                    {
                        fileListList.Add(tempString);
                    }
                    
                }
            }
            long gapCount = 0;
            for (int i = 0; i < fileListList.Count; i++)
            {
                if (fileListList[i].ToString() == ".")
                {
                    gapCount++;
                }
            }

            Console.WriteLine("Count of gaps: " + gapCount);


            //Console.WriteLine(updatedString);
            //fileListList.ForEach(i => Console.Write("{0}\n", i));
            long checksum = 0;
            for (int i = 0; i < fileListList.Count; i++)
            {
                if(fileListList[i].ToString() != ".")
                {
                    checksum = checksum + (i * long.Parse(fileListList[i].ToString()));
                }
            }
            Console.WriteLine("Part A: " + checksum);





















        }
        //022111222
        //024345 12 14 16

        // too low 799772458

        // too low 90994085674
        // wrong 6389916790979

        public string output;
    }
}