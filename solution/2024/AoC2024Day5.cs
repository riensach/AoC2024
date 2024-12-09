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

            HashSet<orderingRule> orderingRulesGroup = new HashSet<orderingRule>();
            HashSet<printingUpdate> printingUpdateGroup = new HashSet<printingUpdate>();          

            foreach (string orderingRoles in topInput)
            {
                string[] inputParams = orderingRoles.Split("|");
                orderingRulesGroup.Add(new orderingRule(int.Parse(inputParams[0]), int.Parse(inputParams[1])));
            }

            foreach (string pagesProduce in secondInput)
            {
                string[] inputParams = pagesProduce.Split(",");
                printingUpdateGroup.Add(new printingUpdate(inputParams, inputParams));

            }

            int middlePages = 0;

            foreach (printingUpdate printingRules in printingUpdateGroup)
            {
                bool validOrder = true;
                foreach (string rulesToCheck in printingRules.pagesUpdate)
                {
                    string currentPage = rulesToCheck;
                    foreach (string checkingPages in printingRules.pagesUpdate)
                    {
                        // HJere is where we check the valid order
                    }




                }
                if(validOrder == true)
                {
                    // Get middle page
                    Double stringLength = (printingRules.pagesUpdate.Length / 2);
                    int findValue = (int) Math.Floor(stringLength);
                    int middlePage = int.Parse(printingRules.pagesUpdate[findValue]);
                    middlePages = middlePages + middlePage;
                }
                
            }

            Console.WriteLine("Part A Middle pages sum: " + middlePages);


            }

        public record orderingRule(int firstPage, int secondPage);
        public record printingUpdate(string[] pagesUpdate, string[] pagesUpdateFixed);

        public string output;
        
    }
}