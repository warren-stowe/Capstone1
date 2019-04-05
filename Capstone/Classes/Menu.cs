using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    class Menu
    {
        /// <summary>
        /// Displays all available products in our Vending Machine
        /// </summary>
        public static void DisplayProducts()
        {
            Console.WriteLine("===============================================");
            foreach (KeyValuePair<string, Tray> product in VendingMachine.Inventory)
            {
                if (!product.Value.OutOfStock)
                {
                    Console.WriteLine($"| {product.Key.PadRight(2)} | {product.Value.Content.Name.PadRight(20)} | {product.Value.Content.Price.ToString().PadRight(4)} | {product.Value.CurrentCapacity.ToString().PadRight(8)} |");
                }
                else
                {
                    Console.WriteLine($"| {product.Key.PadRight(2)} | {product.Value.Content.Name.PadRight(20)} | {product.Value.Content.Price.ToString().PadRight(4)} | SOLD OUT |");
                }
            }
            Console.WriteLine("===============================================\n");
        }

        /// <summary>
        /// Clears the screen, adds our header to the top
        /// </summary>
        public static void ResetScreen()
        {
            string header = "Team 6 Vending Machine\n================";
            Console.Clear();
            Console.WriteLine(header);
        }

        /// <summary>
        /// Displays the Running Balance of the machine
        /// </summary>
        /// <returns></returns>
        public static string DisplayMachineBalance()
        {
            return $"You currently have ${VendingMachine.RunningBalance.ToString("0.00")} in the machine.\n";
        }
    }
}
