using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeLouisville
{
    class Results
    {
        private List<Numbers> AllItems;
        
        // iterate through each item in the numbers list comparing the two dates to one another, if searchDate is greater it will be added to the Results list which will be returned
        public List<Numbers> GetByDate(DateTime searchDate, List<Numbers> allItems)
        {
            AllItems = allItems;
            List<Numbers> Results = new List<Numbers>();

            foreach (var item in AllItems)
            {
                if (DateTime.Compare(item.Date, searchDate) > 0)
                {
                    Results.Add(new Numbers() { Date = item.Date, WinningNumbers = item.WinningNumbers, MegaBall = item.MegaBall, Multiplier = item.Multiplier });
                }
            }
            return Results;
        }

        // iterate through each item in the numbers list comparing string of numbers to client picks, if match is found it will be added to the Results list which will be returned
        public List<Numbers> GetByNumbers(string clientNumbers, List<Numbers> allItems)
        {
            AllItems = allItems;
            List<Numbers> Results = new List<Numbers>();
          
            foreach (var item in AllItems)
            {
                string itemNumbers = string.Format("{0} {1}", item.WinningNumbers, item.MegaBall);
                if (clientNumbers.Equals(itemNumbers))
                {
                    Results.Add(new Numbers() { Date = item.Date, WinningNumbers = item.WinningNumbers, MegaBall = item.MegaBall, Multiplier = item.Multiplier });
                }
            }
            return Results;
        }


        // callIdentifier 
        //  1 = All Records
        //  2 = Records by Date
        //  3 = Records by Number

        // generate a header for each type of call
        // callIdentifier is a int value to simplify and alleviate string comparison issues, it is converted to string value with the ConvertCallIdentifier method
        public string GetHeader(int runCount, int callIdentifier, string query)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--------------------------------")
              .Append(String.Format("\nRun {0}\n{1}", runCount, ConvertCallIdentifier(callIdentifier)))
              .Append("\n--------------------------------\n")
              .Append(String.Format("Your Query Was: {0}", query))
              .Append("\n--------------------------------\n");

            return sb.ToString();
        }

        // convert call type to string so that it can be used in the header
        private string ConvertCallIdentifier(int callIdentifier)
        {
            switch (callIdentifier.ToString())
            {
                case "1":
                    return "Export All Records";
                case "2":
                    return "Export Records by Date";
                case "3":
                    return "Export Records by Number";
                default:
                    return "Unknown Call";

            }
        }
    }
}
