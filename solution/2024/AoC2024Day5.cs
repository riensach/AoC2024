using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using static ABI.System.Collections.Generic.IReadOnlyDictionary_Delegates;
using System.Linq;
using Windows.Globalization.DateTimeFormatting;
using System.ComponentModel;
using static AoC2024.solution.AoCDay5;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using LoreSoft.MathExpressions;
using Windows.Data.Xml.Dom;
using System.Collections.Generic;

namespace AoC2024.solution
{
    public class AoCDay5
    {

        public AoCDay5(int selectedPart, string input)
        {
            string[] lines = input.Split("\n\r\n");            
            string[] topInput = lines[0].Split("\n");
            string[] secondInput = lines[1].Split("\n");

            List<orderingRule> orderingRulesGroup = new List<orderingRule>();
            List<printingUpdate> printingUpdateGroup = new List<printingUpdate>();          

            foreach (string orderingRoles in topInput)
            {
                int[] inputParams = orderingRoles.Split("|").Select(int.Parse).ToArray();
                orderingRulesGroup.Add(new orderingRule(inputParams[0], inputParams[1]));
            }

            foreach (string pagesProduce in secondInput)
            {
                int[] inputParams = pagesProduce.Split(",").Select(int.Parse).ToArray();
                printingUpdateGroup.Add(new printingUpdate(inputParams, inputParams));

            }

            int middlePages = 0;
            int middlePagesFalse = 0;

            foreach (printingUpdate printingRules in printingUpdateGroup)
            {
                bool validOrder = true;
                foreach (int rulesToCheck in printingRules.pagesUpdate)
                {
                    int currentPage = rulesToCheck;

                    // Here is where we check the valid order
                    foreach (orderingRule orderingRule in orderingRulesGroup)
                    {
                        if (orderingRule.firstPage == currentPage)
                        {
                            if(printingRules.pagesUpdate.Contains(orderingRule.secondPage) && Array.IndexOf(printingRules.pagesUpdate, orderingRule.secondPage) < Array.IndexOf(printingRules.pagesUpdate, currentPage))
                            {
                            //Invalid
                                validOrder = false;
                            }
                        }

                        if (orderingRule.secondPage == currentPage)
                        {
                            if (printingRules.pagesUpdate.Contains(orderingRule.secondPage) && Array.IndexOf(printingRules.pagesUpdate, orderingRule.secondPage) > Array.IndexOf(printingRules.pagesUpdate, currentPage))
                            {
                                //Invalid
                                    validOrder = false;
                            }
                        }
                    }





                }
                if(validOrder == true)
                {
                    // Get middle page
                    Double stringLength = (printingRules.pagesUpdate.Length / 2);
                    int findValue = (int) Math.Floor(stringLength);
                    int middlePage = printingRules.pagesUpdate[findValue];
                    middlePages = middlePages + middlePage;
                    continue;
                }



                validOrder = false;
                int index1 = 0;
                int index2 = 0;
                while (validOrder == false)
                {
                    validOrder = true;
                    foreach (int rulesToCheck in printingRules.pagesUpdate)
                    {
                        int currentPage = rulesToCheck;

                        // Here is where we check the valid order
                        foreach (orderingRule orderingRule in orderingRulesGroup)
                        {
                            if (orderingRule.firstPage == currentPage)
                            {
                                if (printingRules.pagesUpdate.Contains(orderingRule.secondPage) && Array.IndexOf(printingRules.pagesUpdate, orderingRule.secondPage) < Array.IndexOf(printingRules.pagesUpdate, currentPage))
                                {
                                    //Invalid
                                    validOrder = false;
                                    index1 = Array.IndexOf(printingRules.pagesUpdate, orderingRule.secondPage);
                                    index2 = Array.IndexOf(printingRules.pagesUpdate, orderingRule.firstPage);
                                    printingRules.pagesUpdate[index1] = orderingRule.firstPage;
                                    printingRules.pagesUpdate[index2] = orderingRule.secondPage;
                                }
                            }

                            if (orderingRule.secondPage == currentPage)
                            {
                                if (printingRules.pagesUpdate.Contains(orderingRule.secondPage) && Array.IndexOf(printingRules.pagesUpdate, orderingRule.secondPage) > Array.IndexOf(printingRules.pagesUpdate, currentPage))
                                {
                                    //Invalid
                                    validOrder = false;
                                    index1 = Array.IndexOf(printingRules.pagesUpdate, orderingRule.firstPage);
                                    index2 = Array.IndexOf(printingRules.pagesUpdate, orderingRule.secondPage);
                                    printingRules.pagesUpdate[index1] = orderingRule.secondPage;
                                    printingRules.pagesUpdate[index2] = orderingRule.firstPage;
                                }
                            }
                        }
                    }
                }


                if (validOrder == true)
                {
                    // Get middle page - I haven't fixed the logic on this yet
                    Double stringLength = (printingRules.pagesUpdate.Length / 2);
                    int findValue = (int)Math.Floor(stringLength);
                    int middlePageFalse = printingRules.pagesUpdate[findValue];
                    middlePagesFalse = middlePagesFalse + middlePageFalse;
                }

            }

            Console.WriteLine("Part A Middle pages sum: " + middlePages);
            Console.WriteLine("Part B Middle pages sum: " + middlePagesFalse);


            }

        public record orderingRule(int firstPage, int secondPage);
        public record printingUpdate(int[] pagesUpdate, int[] pagesUpdateFixed);

        public string output;
        
    }
}