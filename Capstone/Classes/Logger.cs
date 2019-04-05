using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public static class Logger
    {
        private static Dictionary<string, int> SalesHistory = new Dictionary<string, int>();
        private static decimal _TotalSales = 0.00m;
        
        //Read our SalesReport, assign to dictionary, update with new sales, rewrite to salesreport

            /// <summary>
            /// This method runs if the salesreport.txt file does not exist.  It reads from the csv file
            /// and adds all products to a new salesreport.txt file with a starting sale amount of 0.  This
            /// allows the vending machine owner to add new products to the vending machine.
            /// </summary>
            /// <param name="filepath"></param>
        public static void InitializeSalesHistory(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] product = line.Split('|');

                    SalesHistory.Add(product[1], 0);
                }
            }
        }

            /// <summary>
            /// Reads the salesreport.txt file and assigns all values to the SalesHistory dictionary.  This will be
            /// updated via UpdateSalesHistory and re-written to the file in PrintSalesHistory
            /// </summary>
        public static void ReadSalesHistory(string filepath)
        {
            if (!File.Exists("salesreport.txt"))
            {
                InitializeSalesHistory(filepath);
            }
            else
            {

                using (StreamReader sr = new StreamReader("salesreport.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        // If the line contains a pipe, split the line into an array, add it to the dictionary
                        if (line.Contains("|"))
                        {
                            string[] productArray = line.Split("|");
                            SalesHistory.Add(productArray[0], int.Parse(productArray[1]));
                        }
                        else if (line.Contains("$"))    // If the line contains a dollar sign, format the line to leave only the number chars
                        {                                 // parse it to a decimal for purposes of updating _TotalSales

                            line = line.Remove(0, 17);
                            line = line.Replace(",", "");
                            _TotalSales = decimal.Parse(line);
                        }
                        else { }


                    }
                }
            }
        }

        /// <summary>
        /// Re-writes the salesreport.txt file
        /// </summary>
        /// <param name="customerPurchases"></param>
        public static void PrintSalesHistory()
        {
            using (StreamWriter sw = new StreamWriter("salesreport.txt"))
            {
                foreach(KeyValuePair<string, int> sale in SalesHistory)
                {
                    sw.WriteLine($"{sale.Key}|{sale.Value}");
                }

                sw.WriteLine();
                string totalSales = String.Format("{0:n}", _TotalSales);
                sw.WriteLine($"**TOTAL SALES** ${totalSales}");
            }
        }

        /// <summary>
        /// Adds the current customer's purchases to the SalesHistory dictionary
        /// </summary>
        /// <param name="customerPurchases"></param>
        public static void UpdateSalesHistory(List<Product> customerPurchases)
        { 
            foreach (Product product in customerPurchases)
            {
                if (SalesHistory.ContainsKey(product.Name))
                {
                    SalesHistory[product.Name] += 1;
                }

                _TotalSales += product.Price;
            }
        }

        public static void UpdateAuditFile(string action, decimal cost, decimal balance)
        {
            using (StreamWriter sw = new StreamWriter("Log.txt", true))
            {
                action = action + ":";
                sw.WriteLine($"{DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss tt").PadRight(25)} {action.PadRight(25)} ${cost.ToString("0.00").PadRight(8)} ${balance.ToString("0.00")}");
            }
        }
    }
}
