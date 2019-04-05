using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class CoinDispenserTests
    {

        [TestMethod]
        public void GetQuartersTest()
        {
            decimal balance = .75m;
            int expectedQuarters = 3;

            CoinDispenser.DispenseChange(balance);

            Assert.AreEqual(expectedQuarters, CoinDispenser.quarterReturn, $"GetQuarters returned {CoinDispenser.quarterReturn}, it should be {expectedQuarters}.");
        }

        [TestMethod]
        public void GetDimesTest()
        {
            decimal balance = .70m;
            int expectedDimes = 2;

            CoinDispenser.DispenseChange(balance);

            Assert.AreEqual(expectedDimes, CoinDispenser.dimeReturn, $"GetDimes returned {CoinDispenser.dimeReturn}, it should be {expectedDimes}.");
        }

        [TestMethod]
        public void GetNickelsTest()
        {
            decimal balance = .55m;
            int expectedNickels = 1;

            CoinDispenser.DispenseChange(balance);

            Assert.AreEqual(expectedNickels, CoinDispenser.nickelReturn, $"GetNickels returned {CoinDispenser.nickelReturn}, it should be {expectedNickels}.");
        }

        [TestMethod]
        public void GetAllCoins()
        {
            decimal balance1 = 1.55m;
            int expectedQuarters1 = 6;
            int expectedDimes1 = 0;
            int expectedNickels1 = 1;

            decimal balance2 = .20m;
            int expectedQuarters2 = 0;
            int expectedDimes2 = 2;
            int expectedNickels2 = 0;

            decimal balance3 = 2.05m;
            int expectedQuarters3 = 8;
            int expectedDimes3 = 0;
            int expectedNickels3 = 1;

            CoinDispenser.DispenseChange(balance1);
            Assert.AreEqual(expectedQuarters1, CoinDispenser.quarterReturn, $"Quarters returned is {CoinDispenser.quarterReturn}, it should be {expectedQuarters1}.");
            Assert.AreEqual(expectedDimes1, CoinDispenser.dimeReturn, $"Dimes returned {CoinDispenser.dimeReturn}, it should be {expectedDimes1}.");
            Assert.AreEqual(expectedNickels1, CoinDispenser.nickelReturn, $"Nickels returned {CoinDispenser.nickelReturn}, it should be {expectedNickels1}.");

            CoinDispenser.DispenseChange(balance2);
            Assert.AreEqual(expectedQuarters2, CoinDispenser.quarterReturn, $"Quarters returned is {CoinDispenser.quarterReturn}, it should be {expectedQuarters2}.");
            Assert.AreEqual(expectedDimes2, CoinDispenser.dimeReturn, $"Dimes returned {CoinDispenser.dimeReturn}, it should be {expectedDimes2}.");
            Assert.AreEqual(expectedNickels2, CoinDispenser.nickelReturn, $"Nickels returned {CoinDispenser.nickelReturn}, it should be {expectedNickels2}.");

            CoinDispenser.DispenseChange(balance3);
            Assert.AreEqual(expectedQuarters3, CoinDispenser.quarterReturn, $"Quarters returned is {CoinDispenser.quarterReturn}, it should be {expectedQuarters3}.");
            Assert.AreEqual(expectedDimes3, CoinDispenser.dimeReturn, $"Dimes returned {CoinDispenser.dimeReturn}, it should be {expectedDimes3}.");
            Assert.AreEqual(expectedNickels3, CoinDispenser.nickelReturn, $"Nickels returned {CoinDispenser.nickelReturn}, it should be {expectedNickels3}.");
        }
            
        [TestMethod]
        public void CheckFor0OrNegativeBalance()
        {
            decimal balance = 0;
            Assert.AreEqual(false, CoinDispenser.DispenseChange(balance), $"{balance} is 0, DispenseChange should return false.");

            decimal balance2 = -15;
            Assert.AreEqual(false, CoinDispenser.DispenseChange(balance2), $"Negative banace should return false.");
        }
    }
}
