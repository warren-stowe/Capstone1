using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.IO;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestInitialize]
        public void InitializeInventory()
        {
            VendingMachine.RunningBalance = 10.00M;

            string currentDir = Environment.CurrentDirectory;
            string filename = "vendingmachine.csv";
            string filePath = Path.Combine(currentDir, filename);

            if (VendingMachine.Inventory.Count == 0)
            {
                VendingMachine.InitializeInventory(filePath);
            }
        }

        [TestMethod]
        public void InitializeInventoryKeysTest()
        {
            string key1 = "A1";
            string key2 = "C2";
            string key3 = "D3";

            Assert.IsTrue(VendingMachine.Inventory.ContainsKey(key1), $"The inventory did not correctly initialize.  {key1} is not an entry.");
            Assert.IsTrue(VendingMachine.Inventory.ContainsKey(key2), $"The inventory did not correctly initialize.  {key2} is not an entry.");
            Assert.IsTrue(VendingMachine.Inventory.ContainsKey(key3), $"The inventory did not correctly initialize.  {key3} is not an entry.");
        }

        [TestMethod]
        public void InitializeInventoryValuesTest()
        {
            bool valueIsPresent = false;

            foreach (KeyValuePair<string, Tray> tray in VendingMachine.Inventory)
            {
                if (tray.Value.Content.Name == "Potato Crisps")
                {
                    valueIsPresent = true;
                }
            }

            Assert.IsTrue(valueIsPresent, $"The inventory did not correctly initialize.  Potato Crisps is not a value.");
        }

        [TestMethod]
        public void FeedMoneyValidInputsTest()
        {
            string input1 = "1";
            string input2 = "2";
            string input3 = "5";
            string input4 = "10";

            Assert.AreEqual(true, VendingMachine.FeedMoney(input1), $"Input of {input1} should return true.");
            Assert.AreEqual(true, VendingMachine.FeedMoney(input2), $"Input of {input2} should return true.");
            Assert.AreEqual(true, VendingMachine.FeedMoney(input3), $"Input of {input3} should return true.");
            Assert.AreEqual(true, VendingMachine.FeedMoney(input4), $"Input of {input4} should return true.");
        }

        [TestMethod]
        public void FeedMoneyInvalidInputsTest()
        {
            string input1 = "4";
            string input2 = "kdhgdfg";
            string input3 = "5.0";

            Assert.AreEqual(false, VendingMachine.FeedMoney(input1), $"Input of {input1} should return false.");
            Assert.AreEqual(false, VendingMachine.FeedMoney(input2), $"Input of {input2} should return false.");
            Assert.AreEqual(false, VendingMachine.FeedMoney(input3), $"Input of {input3} should return false.");
        }

        [TestMethod] 
        public void FeedMoneyRunningBalanceTest()
        {
            VendingMachine.FeedMoney("5");

            Assert.AreEqual(15.00M, VendingMachine.RunningBalance, "Five dollars added to balance of 10, output should be $15.00M.");
        }
    
        [TestMethod]
        public void SelectProductValidInputsTest()
        {

            string input1 = "A1";
            string input2 = "B2";
            string input3 = "C3";

            Assert.AreEqual(true, VendingMachine.SelectProduct(input1), $"Input of {input1} should return true.");
            Assert.AreEqual(true, VendingMachine.SelectProduct(input2), $"Input of {input2} should return true.");
            Assert.AreEqual(true, VendingMachine.SelectProduct(input3), $"Input of {input3} should return true.");
        }

        [TestMethod]
        public void SelectProductInvalidInputsTest()
        {
            string input1 = "D6";
            string input2 = "fjkhfg";

            Assert.AreEqual(false, VendingMachine.SelectProduct(input1), $"Input of {input1} should return false.");
            Assert.AreEqual(false, VendingMachine.SelectProduct(input2), $"Input of {input2} should return false.");
        }

        [TestMethod] 
        public void SelectProductOutOfStockTest()
        {
            VendingMachine.Inventory["A1"].CurrentCapacity = 0;
            VendingMachine.Inventory["A2"].CurrentCapacity = 1;

            Assert.AreEqual(false, VendingMachine.SelectProduct("A1"), "The machine is out of this product, SelectProduct should return false.");
            Assert.AreEqual(true, VendingMachine.SelectProduct("A2"), "Returned true, but the product is in stock.");
        }

        [TestMethod]
        public void SelectProductRunningBalanceTest()
        {
            VendingMachine.SelectProduct("B2");
            Assert.AreEqual(8.50M, VendingMachine.RunningBalance, "Running balance did not decrement the correct amount (10.00 - 1.50).");
        }

        [TestMethod]
        public void SelectProductCurrentCapacityTest()
        {
            
            VendingMachine.SelectProduct("D1");
            Assert.AreEqual(4, VendingMachine.Inventory["D1"].CurrentCapacity, "Select product did not decrement the CurrentCapacity of the product.");
        }

        [TestMethod]
        public void FinishTransactionBalanceTest()
        {
            VendingMachine.FinishTransaction();
            Assert.AreEqual(0, VendingMachine.RunningBalance, "Finish Transaction did not turn Running Balance to 0.");
        }
    }
}
