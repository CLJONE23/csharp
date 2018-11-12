using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace codeLouisville
{
    class WriteToFile
    {
        // this is public so that methods in Program class can access it
        public readonly string OutFileLocation = string.Format("{0}\\LotteryResults.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

        // new will be used if it is the first run since the program was executed.  this will overwrite the existing results file.
        public void New(List<Numbers> results, string header, int callIdentifier)
        {
            //overwrite
            using (StreamWriter writer = new StreamWriter(OutFileLocation))
            {
                //write standard header
                writer.WriteLine(header);
                //handle cases where there are and are not results
                if (results.Count > 0)
                {
                    //write unique message if users is checking their favorite numbers
                    if (callIdentifier == 3)
                    {
                        writer.WriteLine("You've got some winners!");
                    }
                    //write results to file
                    foreach (var result in results)
                    {
                        writer.WriteLine(result.ToString());
                    }
                }
                else
                {
                    //write unique message if users is checking their favorite numbers
                    if (callIdentifier == 3)
                    {
                        writer.WriteLine("Sorry! Your numbers have never won.");
                    }
                    else
                    {
                        writer.WriteLine("No Items Found!");
                    }
                }
            }
        }

        // append will be used if the run count is greater than 0, it will append results to the results file.
        public void Append(List<Numbers> results, string header, int callIdentifier)
        {
            //append instead of overwrite
            using (StreamWriter writer = File.AppendText(OutFileLocation))
            {
                //add padding since appending
                writer.WriteLine("\n");
                writer.WriteLine(header);
                //handle cases where there are and are not results
                if (results.Count > 0)
                {
                    //write unique message if users is checking their favorite numbers
                    if (callIdentifier == 3)
                    {
                        writer.WriteLine("You've got some winners!");
                    }
                    //write results to file
                    foreach (var result in results)
                    {
                        writer.WriteLine(result.ToString());
                    }
                }
                else
                {
                    //write unique message if users is checking their favorite numbers
                    if (callIdentifier == 3)
                    {
                        writer.WriteLine("Sorry! Your numbers have never won.");
                    }
                    else
                    {
                        writer.WriteLine("No Items Found!");
                    }
                }
            }
        }
    }
}