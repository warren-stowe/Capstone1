using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class FileTests
    {
        [TestMethod]
        public void SalesReportFileExistsTest()
        {
            string currentDir = Environment.CurrentDirectory;
            string filename = "vendingmachine.csv";
            string filePath = Path.Combine(currentDir, filename);

            Logger.ReadSalesHistory(filename);
            Logger.PrintSalesHistory();

            Assert.IsTrue(File.Exists("salesreport.txt"), "Should always return true. vendingmachine.csv is not in the current directory");
        }

        [TestMethod]
        public void InputFileExistsTest()
        {
            Assert.IsTrue(File.Exists("vendingmachine.csv"), "Should always return true. vendingmachine.csv is not in the current directory.");
        }
    }
}
