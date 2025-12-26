using System;
using System.Diagnostics.Contracts;

namespace HelloWorld
{
    internal class Methods
    {
        public static void Run()
        {
            int[] intsToCompress = new int[] { 10, 15, 20, 25, 30 };
            int totalValue = 0;
            DateTime startTime = DateTime.Now;

            totalValue = getSum(intsToCompress);

            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine($"Total value with method: {totalValue}");

            int[] intsToCompress2 = new int[] { 30, 35, 40, 45, 50 };

            totalValue = getSum(intsToCompress2);
            
            Console.WriteLine(totalValue);
        }

        static private int getSum(int[] intsToCompress)
        {
            int totalValue = 0;
            foreach (int intForCompress in intsToCompress)
                {
                    totalValue += intForCompress;
                }
           return totalValue;
        }
    }
}