using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{

    /// <summary>
    /// This subclass of Product occurs in the default case during initialize inventory.  If a new product
    /// type is added to the CSV file, it will default to this class.
    /// </summary>
    public class NewProductType : Product
    {
        public NewProductType(string name, decimal price) : base(name, price)
        {
            Sound = "Mmmm";
        }
    }
}

