using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public static class VendingMachine
    {
        public static decimal RunningBalance { get; set; }
        public static Dictionary<string, Tray> Inventory = new Dictionary<string, Tray>();
        public static List<Tray> Trays = new List<Tray>();

        /// <summary>
        /// Creates an inventory of vending machine objects from a given file
        /// </summary>
        /// <param name="filepath"></param>
        public static void InitializeInventory(string filepath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    while (!sr.EndOfStream)
                    {
                        //Reads each line, separating them by '|'
                        string line = sr.ReadLine();
                        string[] productProperties = line.Split('|');

                        //Creates new objects based on the type, adds them to our trays, adds those trays to our inventory
                        switch (productProperties[3])
                        {
                            case "Chip":
                                Tray chipTray = new Tray(new Chips(productProperties[1], decimal.Parse(productProperties[2])));
                                Inventory.Add(productProperties[0], chipTray);
                                break;

                            case "Candy":
                                Tray candyTray = new Tray(new Candy(productProperties[1], decimal.Parse(productProperties[2])));
                                Inventory.Add(productProperties[0], candyTray);
                                break;

                            case "Drink":
                                Tray drinkTray = new Tray(new Drink(productProperties[1], decimal.Parse(productProperties[2])));
                                Inventory.Add(productProperties[0], drinkTray);
                                break;

                            case "Gum":
                                Tray gumTray = new Tray(new Gum(productProperties[1], decimal.Parse(productProperties[2])));
                                Inventory.Add(productProperties[0], gumTray);
                                break;

                            default:
                                Tray defaultTray = new Tray(new NewProductType(productProperties[1], decimal.Parse(productProperties[2])));
                                Inventory.Add(productProperties[0], defaultTray);
                                break;
                        }
                    }

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("vendingmachine.csv does not exist. Please make sure it is in the same directory you are running this program from.");
                throw;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("vendingmachine.csv is not in the approriate format. Please make sure each line is pipe delimited like so: A1|SnackName|0.00|SnackType");
                throw;
            }
        }

        /// <summary>
        /// Checks to see if the user gave a valid quantity of cash, increments the current balance of the machine
        /// </summary>
        /// <param name="userInput"></param>
        public static bool FeedMoney(string userInput)
        {
            if (userInput == "1" || userInput == "2" || userInput == "5" || userInput == "10")
            {               
                RunningBalance += decimal.Parse(userInput);
                Logger.UpdateAuditFile("FEED MONEY", decimal.Parse(userInput), RunningBalance);
                Menu.ResetScreen();
                Console.WriteLine($"${userInput} have been added to the machine...\n");
                return true;
            }
            else
            {
                if (userInput != "x")
                {
                    Console.WriteLine($"*spits the {userInput} back at you* Please insert a valid bill...\n");
                }
                return false;
            }
        }

        /// <summary>
        /// Removes a given product from the vending machine, adds it to the user's purchase list
        /// </summary>
        /// <param name="Inventory Key"></param>
        /// <returns></returns>
        public static bool SelectProduct(string userInput)
        {
            Console.Clear();
            foreach (KeyValuePair<string, Tray> product in Inventory)
            {
                if (userInput.ToLower() == product.Key.ToLower())
                {
                    if (RunningBalance > product.Value.Content.Price)
                    {
                        if (product.Value.CurrentCapacity > 0)
                        {
                            RunningBalance -= product.Value.Content.Price;
                            product.Value.CurrentCapacity -= 1;
                            Customer.Purchases.Add(product.Value.Content);
                            Logger.UpdateAuditFile($"{product.Value.Content.Name} {product.Key}", product.Value.Content.Price, RunningBalance);
                            Menu.ResetScreen();
                            Console.WriteLine($"You have received {product.Value.Content.Name}\n");
                            return true;
                        }
                        else
                        {
                            Menu.ResetScreen();
                            Console.WriteLine($"{product.Value.Content.Name} is out of stock...\n");
                            return false;
                        }
                    }
                    else
                    {
                        Menu.ResetScreen();
                        Console.WriteLine($"You do not have enough money to purchase {product.Value.Content.Name}\n");
                        return false;
                    }
                }
            }
            Menu.ResetScreen();
            Console.WriteLine($"{userInput} is not an existing tray number...\n");
            return false;
        }

        /// <summary>
        /// Finalizes the transaction, returns the remaining balance to the user
        /// </summary>
        public static void FinishTransaction()
        {
            Menu.ResetScreen();
            Logger.UpdateAuditFile($"GIVE CHANGE", RunningBalance, 0.00m);
            CoinDispenser.DispenseChange(RunningBalance);
            RunningBalance = 0;
            foreach (Product purchase in Customer.Purchases)
            {
                Console.WriteLine($"Enjoy your {purchase.Name}! {purchase.ConsumeItem()}");
            }
            Console.WriteLine("\nThank you for using Team 6's Vending Machine!");
        }
    }
}


