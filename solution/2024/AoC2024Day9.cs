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
            List<File> fileListObj = new List<File>();
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
                        fileListList.Add(id.ToString());
                    }

                    fileList.Add(i, id.ToString());
                    fileListObj.Add(new File(1,id,blockSize));
                    newString = newString + tempString;
                } else
                {
                    // free spacetempString = "";
                    tempString = "";
                    for (int x = 0; x < blockSize; x++)
                    {
                        tempString = tempString + ".";
                        fileListList.Add(".");
                    }
                    newString = newString + tempString;
                    fileList.Add(i, ".");
                    if(blockSize > 0)
                    {
                        fileListObj.Add(new File(0, id, blockSize));
                    }
                    
                }
            }
            //Console.WriteLine(newString);
            foreach (KeyValuePair<int, string> kvp in fileList)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            //fileListList.ForEach(i => Console.Write("{0}\n", i));
            //fileListObj.ForEach(i => Console.Write("{0} - {1} - {2}\n", i.type,i.id,i.length));
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

            //Console.WriteLine("Count of gaps: " + gapCount);


            //Console.WriteLine(updatedString);
            //fileListList.ForEach(i => Console.Write("{0}\n", i));
            long checksum = 0;
            for (int i = 0; i < fileListList.Count; i++)
            {
                if (fileListList[i].ToString() != ".")
                {
                    checksum = checksum + (i * long.Parse(fileListList[i].ToString()));
                }
            }
            Console.WriteLine("Part A: " + checksum);



            //List<File> fileListObjCopy = new List<File>(fileListObj);

            List<int> attemptedToMove = new List<int>();


            for (int i = fileListObj.Count - 1; i > 0; i--)
            {
                //fileListObjCopy = new List<File>(fileListObj);
                //Console.WriteLine("Moiving file1 " + fileListObj[i].id + " with iterator " + i);
                if (fileListObj[i].type == 1 && !attemptedToMove.Contains(fileListObj[i].id))
                {
                    //fileListObj.ForEach(i => Console.Write("{0} - {1} - {2}\n", i.type, i.id, i.length));
                    //Console.WriteLine("---------");
                   // fileListObjCopy.ForEach(i => Console.Write("{0} - {1} - {2}\n", i.type, i.id, i.length));
                    Console.WriteLine("Moiving file " + fileListObj[i].id);
                    attemptedToMove.Add(fileListObj[i].id);

                    for (int x = 0; x < fileListObj.Count; x++)
                    {
                        if (i <= x) continue;
                        if (fileListObj[x].type == 0 && fileListObj[x].length >= fileListObj[i].length)
                        {
                            // We can move. We need to also see if we need to split object
                            if(fileListObj[x].length > fileListObj[i].length)
                            {
                                // Need to leave current object with some remaining
                                //Console.WriteLine("Need to leave current object with some remaining - " + x + " - " + i);
                                int space = fileListObj[i].length;
                                int id = fileListObj[i].id;
                                fileListObj[x].length = fileListObj[x].length - fileListObj[i].length;
                                //fileListObj.RemoveAt(i);

                                // Putting back the spare space?--
                                //fileListObj.Insert(i + 1, new File(0, 0, space));

                                if (fileListObj.ElementAtOrDefault(i + 1) != null && fileListObj[i + 1].type == 0 && fileListObj.ElementAtOrDefault(i - 1) != null && fileListObj[i - 1].type == 0)
                                {
                                    //Console.WriteLine("here1");
                                    fileListObj[i].length = fileListObj[i].length + fileListObj[i - 1].length + fileListObj[i + 1].length;
                                    fileListObj[i].type = 0;
                                    fileListObj[i].id = 0;
                                    fileListObj.RemoveAt(i + 1);
                                    fileListObj.RemoveAt(i - 1);
                                }
                                else if (fileListObj.ElementAtOrDefault(i - 1) != null && fileListObj[i - 1].type == 0)
                                {
                                    //Console.WriteLine("here2");
                                    fileListObj[i - 1].length = fileListObj[i - 1].length + space;
                                    fileListObj.RemoveAt(i);
                                }
                                else if (fileListObj.ElementAtOrDefault(i + 1) != null && fileListObj[i + 1].type == 0)
                                {
                                    //Console.WriteLine("here3");
                                    fileListObj[i + 1].length = fileListObj[i + 1].length + space;
                                    fileListObj.RemoveAt(i);
                                }
                                else
                                {
                                    //Console.WriteLine("here4");
                                    fileListObj[i].type = 0;
                                    fileListObj[i].id = 0;
                                }
                                //Console.WriteLine("found here - " + x + " - " + i);
                                //Console.WriteLine("{0} - {1} - {2}\n", fileListObjCopy[i].type, fileListObjCopy[i].id, fileListObjCopy[i].length);
                                fileListObj.Insert(x, new File(1, id, space));

                            }
                            else
                            {
                                // Straight up move
                                //Console.WriteLine("Straight up move - " + x + " - " + i);
                                int space = fileListObj[i].length;
                                fileListObj[x].type = 1;
                                fileListObj[x].id = fileListObj[i].id;
                                //fileListObj.RemoveAt(i);
                                //fileListObj.Insert(x, fileListObjCopy[i]);
                                // Putting back the spare space?
                                //fileListObj.Insert(i + 1, new File(0, 0, space));


                                if (fileListObj.ElementAtOrDefault(i + 1) != null && fileListObj[i + 1].type == 0 && fileListObj.ElementAtOrDefault(i - 1) != null && fileListObj[i - 1].type == 0)
                                {
                                    //Console.WriteLine("here1");
                                    fileListObj[i].length = fileListObj[i].length + fileListObj[i - 1].length + fileListObj[i + 1].length;
                                    fileListObj[i].type = 0;
                                    fileListObj[i].id = 0;
                                    fileListObj.RemoveAt(i+1);
                                    fileListObj.RemoveAt(i-1);
                                }
                                else if (fileListObj.ElementAtOrDefault(i - 1) != null && fileListObj[i - 1].type == 0)
                                {
                                    //Console.WriteLine("here2");
                                    fileListObj[i - 1].length = fileListObj[i - 1].length + space;
                                    fileListObj.RemoveAt(i);
                                }
                                else if (fileListObj.ElementAtOrDefault(i + 1) != null && fileListObj[i + 1].type == 0)
                                {
                                    //Console.WriteLine("here3");
                                    fileListObj[i + 1].length = fileListObj[i + 1].length + space;
                                    fileListObj.RemoveAt(i);
                                }
                                else
                                {
                                    //Console.WriteLine("here4");
                                    fileListObj[i].type = 0;
                                    fileListObj[i].id = 0;
                                }
                            }
                            break;
                        }
                    }
                }


            }





            fileListObj.ForEach(i => Console.Write("{0} - {1} - {2}\n", i.type, i.id, i.length));










            //Console.WriteLine(updatedString);
            //fileListList.ForEach(i => Console.Write("{0}\n", i));
            checksum = 0;
            int multi = 0;
            for (int i = 0; i < fileListObj.Count; i++)
            {
                if (fileListObj[i].type == 1)
                {
                    for (int x = 0; x < fileListObj[i].length; x++)
                    {
                        checksum = checksum + (multi * long.Parse(fileListObj[i].id.ToString()));
                        multi++;
                    }

                    
                } else
                {
                    for (int x = 0; x < fileListObj[i].length; x++)
                    {
                        
                        multi++;
                    }
                }
            }
            Console.WriteLine("Part B: " + checksum);

            // 6436094330816 too high
            // 6448830362223 too high


        }
        //022111222
        //024345 12 14 16

        // too low 799772458

        // too low 90994085674
        // wrong 6389916790979
        public class File
        {
            public int type;
            public int id;
            public int length;
            public File(int typeInput, int idInput, int lengthInput)
            {
                type = typeInput;
                id = idInput;
                length = lengthInput;

            }

        }


        public string output;
    }
}