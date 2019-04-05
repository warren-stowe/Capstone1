using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    interface IConsumable
    {
        // Holds the sound made by the product when consumed (i.e., "glug", "crunch")
        string Sound { get; }
        string Type { get; }

        // Returns a string representing the sound of the product being consumed (i.e., "Glug glug, Yum!")
        string ConsumeItem();

    }
}
