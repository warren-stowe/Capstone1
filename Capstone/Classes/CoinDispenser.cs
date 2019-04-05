using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public static class CoinDispenser
    {
        public const int QUARTERS = 25;
        public const int DIMES = 10;
        public const int NICKELS = 5;

        public static int quarterReturn = 0;
        public static int dimeReturn = 0;
        public static int nickelReturn = 0;

        /// <summary>
        /// Dispenses change for FinishTransaction.  A $0 balance will exit the method to avoid outputting 
        /// the final string.
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        public static bool DispenseChange(decimal balance)
        {

            if (balance <= 0)
            {
                return false;
            }

            int balanceToChange = (int)(balance * 100);

            quarterReturn = balanceToChange / QUARTERS;
            balanceToChange = balanceToChange % QUARTERS;

            dimeReturn = balanceToChange / DIMES;
            balanceToChange = balanceToChange % DIMES;

            nickelReturn = balanceToChange / NICKELS;
            balanceToChange = balanceToChange % NICKELS;

            
            Console.WriteLine($"Dispensing {quarterReturn} quarters, {dimeReturn} dimes, and {nickelReturn} nickels.\n");
            return true;
        }

    }
}
