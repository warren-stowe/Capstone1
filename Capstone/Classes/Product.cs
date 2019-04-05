using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public abstract class Product : IConsumable
    {
        public string Sound { get; protected set; } = "";
        public string Type { get; protected set; } = "";

        protected string _Name;
        public string Name
        {
            get { return _Name; }
        }

        protected decimal _Price;
        public decimal Price
        {
            get { return _Price; }
        }
      

        public Product() { }

        public Product(string name, decimal price)
        {
            _Name = name;
            _Price = price;
        }

        /// <summary>
        /// Provides the final output for Finish Transaction.
        /// </summary>
        /// <returns></returns>
        public string ConsumeItem()
        {
            return $"{Sound} {Sound}, Yum!";
        }
    }
}
