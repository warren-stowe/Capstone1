using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chips : Product
    {
        public Chips(string name, decimal price) : base(name, price)
        {
            Sound = "Crunch";
            Type = "Chips";
        }
    }
}
