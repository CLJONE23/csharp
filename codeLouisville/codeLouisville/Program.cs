using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace codeLouisville
{
    class Program
    {

        

        static void Main(string[] args)
        {
            // common variables
            int RunCount = 0;
            List<Numbers> results;

            // Declare out here because it is needed outside of the usings
            List<Numbers> numbers;

            // csv reader needs a stream, so declare that one first then pass that into the csv reader
            using (var streamReader = new StreamReader(@"..\..\data.csv"))
            using (var reader = new CsvReader(streamReader))
            {
                // Register the custom score map
                // That takes care of how the csv file will be mapped to Score
                reader.Configuration.RegisterClassMap<NumbersMap>();

                // Read all of the records and put them into a list
                numbers = reader.GetRecords<Numbers>()
                    .ToList();
            }

            // Create the menu string
            StringBuilder menu = new StringBuilder();
            menu.Append("\nMegaBall Lottery Drawings since 1992\n")
                .Append("--------------------------------------")
                .Append("\nMenu\n")
                .Append("--------------------------------------")
                .Append("\nEnter 1 to export all records")
                .Append("\nEnter 2 to export all records after a specific date")
                .Append("\nEnter 3 compare your favorite numbers to past winning numbers")
                .Append("\nEnter Q to quit")
                .Append("\n")
                .Append("--------------------------------------")
                .Append("\nEnter your selection:");

            // Write the menu
            Console.WriteLine(menu.ToString());

            // Get the first input
            var input = Console.ReadLine();

            // instance of the Results class for getting results
            Results getResults = new Results();
            

            // Keep going unless someone enters Q
            while (input.ToUpper() != "Q")
            {
                switch (input)
                {
                    // Print the list
                    case "1":
                        results = numbers;
                        PrintList(results, RunCount, 1, "All Records");
                        WriteResultsToFile(results, RunCount, 1, "All Records");
                        RunCount++;
                        ReturnToMenu(menu.ToString());
                        break;
                    // If the command isn't one of the designated values, print a nice message
                    case "2":
                        Console.WriteLine("\nPlease enter a date (mm/dd/yyyy): ");
                        bool successDateTime = DateTime.TryParse(Console.ReadLine(), out DateTime inputDate);
                        if(successDateTime)
                        {
                            results = getResults.GetByDate(inputDate, numbers);
                            PrintList(results, RunCount, 2, inputDate.ToString());
                            WriteResultsToFile(results, RunCount, 2, inputDate.ToString());
                            RunCount++;
                            ReturnToMenu(menu.ToString());
                            break;
                        }
                        else
                        {
                            Console.WriteLine("******************************\nNOT A VALID DATE\n******************************");
                            continue;
                        }
                    case "3":
                        // store picks in a list as integers so that they can be sorted and to ensure that the same number is not picked twice
                        List<int> picks = new List<int>();
                        // store megaball separately since it can repeat a number in the pick list
                        string pickMegaBall;
                        // continue loop until you have five good numbers in pick list
                        while (picks.Count < 5)
                        {
                            Console.WriteLine("\nPlease enter a number between 1 and 70:");
                            // make sure input is an integer within the specified range and not already in the picks list
                            bool successInt = int.TryParse(Console.ReadLine(), out int pick);
                            if (successInt && pick > 0 && pick < 71 && (!picks.Contains(pick)))
                            {
                                picks.Add(pick);
                            }
                            else if (picks.Contains(pick))
                            {
                                // explicity tell users why this number was not accepted
                                Console.WriteLine("******************************\nYOU'VE ALREADY CHOSEN THIS NUMBER\n******************************");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("******************************\nNOT A VALID NUMBER\n******************************");
                                continue;
                            }
                        }
                        Console.WriteLine("\nPlease enter a number between 1 and 55:");
                        // make sure input is an integer within the specified range
                        bool successIntMega = int.TryParse(Console.ReadLine(), out int pickMega);
                        if (successIntMega && pickMega > 0 && pickMega < 56)
                        {
                            // convert int to string, if less than zero add leading zero to match dataset
                            if (pickMega < 10)
                            {
                                pickMegaBall = String.Format("0{0}", pickMega.ToString());
                            }
                            else
                            {
                                pickMegaBall = pickMega.ToString();
                            }
                        }
                        else
                        {
                            Console.WriteLine("******************************\nNOT A VALID NUMBER\n******************************");
                            continue;
                        }

                        // sort picks because dataset numbers are sorted
                        picks.Sort();

                        // store final client picks in this string to be compared to dataset
                        string picksString = "";
                        
                        // iterate through picks list converting each value to string and appending to picksString
                        foreach(var pick in picks)
                        {
                            // add leading zero if below 10 to match dataset
                            if (pick < 10)
                            {
                                picksString += string.Format("0{0} ", pick.ToString());
                            }
                            else
                            {
                                picksString += string.Format("{0} ", pick.ToString());
                            }
                        }

                        // add megaball to picksString
                        picksString += pickMegaBall;

                        results = getResults.GetByNumbers(picksString, numbers);
                        PrintList(results, RunCount, 3, picksString);
                        WriteResultsToFile(results, RunCount, 3, picksString);
                        RunCount++;
                        ReturnToMenu(menu.ToString());
                        break;
                    default:
                        Console.WriteLine("Command not recognized");
                        break;
                }

                // Get the input
                input = Console.ReadLine();
            }
        }

        // Print results to console in same fashion they are written to file
        private static void PrintList(List<Numbers> results, int runCount, int callIdentifier, string query)
        {
            Results getResults = new Results();
            Console.WriteLine("\n");
            Console.WriteLine(getResults.GetHeader(runCount, callIdentifier, query));
            if (results.Count > 0)
            {
                if (callIdentifier == 3)
                {
                    Console.WriteLine("You've got some winners!");
                }
                foreach (var result in results)
                {
                    Console.WriteLine(result.ToString());
                }
            }
            else
            {
                if (callIdentifier == 3)
                {
                    Console.WriteLine("Sorry!  Your numbers have never won.");
                }
                else
                {
                    Console.WriteLine("No Items Found!");
                }
            }
            Console.WriteLine("\n");
        }

        // perform same actions after every query
        private static void ReturnToMenu(string menu)
        {
            string DIV = "---------------------------------------";
            WriteToFile write = new WriteToFile();
            Console.WriteLine(String.Format("\n{0}\nYour results have been exported to {1}\n{2}\n\nHit any key to return to the main menu...", DIV, write.OutFileLocation, DIV));
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(menu);
        }

        // pass results to this method so that it can be determined which method in the WriteToFile class should be used
        private static void WriteResultsToFile(List<Numbers> results, int runCount, int callIdentifier, string query)
        {
            WriteToFile writeToFile = new WriteToFile();
            Results getResults = new Results();
            if (runCount == 0)
            {
                writeToFile.New(results, getResults.GetHeader(runCount, callIdentifier, query), callIdentifier);
            }
            else
            {
                writeToFile.Append(results, getResults.GetHeader(runCount, callIdentifier, query), callIdentifier);
            }
        }
    }
}
