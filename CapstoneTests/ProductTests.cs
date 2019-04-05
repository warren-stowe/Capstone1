using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    class ProductTests
    {
        Product Gum = new Gum("X-Tra", 1.00M);
        Product Drink = new Drink("Poopsi Cola", 3.00M);
        Product Chips = new Chips("Don'tidos", 2.00M);
        Product Candy = new Candy("ICantBelieveItsNotButterfingers", 2.00M);

        [TestMethod]
        public void ConsumeItemTest()
        {
            string gumResult = "Chew Chew, Yum!";
            string drinkResult = "Glug Glug, Yum!";
            string chipsResult = "Crunch Crunch, Yum!";
            string candyResult = "Munch Munch, Yum!";

            Assert.AreEqual(gumResult, Gum.ConsumeItem(), "Gum returned the wrong sound.  It should be Chew.");
            Assert.AreEqual(drinkResult, Drink.ConsumeItem(), "Drink returned the wrong sound.  It should be Glug.");
            Assert.AreEqual(chipsResult, Chips.ConsumeItem(), "Chips returned the wrong sound.  It should be crunch.");
            Assert.AreEqual(candyResult, Candy.ConsumeItem(), "Candy returned the wrong sound.  It should be munch.");
        }

    }
}
