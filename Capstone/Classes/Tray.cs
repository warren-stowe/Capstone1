using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Tray
    {
        public const int MAXCAPACITY = 5;
        public int CurrentCapacity { get; set; }
        public Product Content { get; }
        public bool OutOfStock { get { return CurrentCapacity == 0; } }
        
        public Tray(Product content)
        {
            CurrentCapacity = MAXCAPACITY;
            Content = content;
        }

    }
}
