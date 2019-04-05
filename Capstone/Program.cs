using System;
using Capstone.Classes;
using System.Collections.Generic;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            string filename = "vendingmachine.csv";
            string filePath = Path.Combine(currentDir, filename);
            bool programDone = false;
            try
            {
                VendingMachine.InitializeInventory(filePath);
            }
            catch
            {
                programDone = true;
            }
            
            Menu.ResetScreen();
            while (!programDone)
            {
                Console.WriteLine("What would you like to do?\n(1) Display Vending Machine Items\n(2) Purchase\n(3) Quit");
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    Menu.ResetScreen();
                    Menu.DisplayProducts();
                }
                else if (userInput == "2")
                {
                    Menu.ResetScreen();
                    while (!programDone)
                    {
                        Console.WriteLine($"{Menu.DisplayMachineBalance()}What would you like to do?\n(1) Feed Money\n(2) Select Product\n(3) Finish Transaction");
                        userInput = Console.ReadLine();
                        if (userInput == "1")
                        {
                            Menu.ResetScreen();
                            while (userInput != "x")
                            {
                                Console.WriteLine($"{Menu.DisplayMachineBalance()}Please insert a bill (We accept $1, $2, $5, $10) or press 'x' to return to the previous menu...");
                                userInput = Console.ReadLine();
                                Menu.ResetScreen();
                                VendingMachine.FeedMoney(userInput);
                            }
                        }
                        else if (userInput == "2")
                        {
                            Menu.ResetScreen();
                            Menu.DisplayProducts();
                            Console.WriteLine($"{Menu.DisplayMachineBalance()}Please enter a number associated with the item you'd like to receive...");
                            userInput = Console.ReadLine();
                            VendingMachine.SelectProduct(userInput);
                        }
                        else if (userInput == "3")
                        {
                            Menu.ResetScreen();
                            VendingMachine.FinishTransaction();
                            programDone = true;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input, please try again...\n");
                        }
                    }
                }
                else if (userInput == "3")
                {
                    Menu.ResetScreen();
                    VendingMachine.FinishTransaction();
                    programDone = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please try again...\n");
                }
            }

            //Logs sales history, printing the results to our sales file
            Logger.ReadSalesHistory(filename);
            Logger.UpdateSalesHistory(Customer.Purchases);
            Logger.PrintSalesHistory();

            Console.ReadLine();

        }
    }
}
