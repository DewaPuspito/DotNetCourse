using System;

namespace HelloWorld
{
    internal class Loops
    {
        public static void Run()
        {
            int[] intsToCompress = new int[] { 10, 15, 20, 25, 30 };

            DateTime startTime = DateTime.Now;

            int totalValue = intsToCompress[0] + intsToCompress[1] + intsToCompress[2] + intsToCompress[3] + intsToCompress[4];

            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds);

            // Console.WriteLine($"Total value without loop: {totalValue}");



            totalValue = 0;

            startTime = DateTime.Now;

            for (int i = 0; i < intsToCompress.Length; i++)
            {
                totalValue += intsToCompress[i];
            }

            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds);

            // Console.WriteLine($"Total value with loop: {totalValue}");

            totalValue = 0;

            startTime = DateTime.Now;

            foreach (int intForCompress in intsToCompress)
            {
                totalValue += intForCompress;
            }

            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds);

            // Console.WriteLine($"Total value with foreach loop: {totalValue}");

            totalValue = 0;

            startTime = DateTime.Now;

            int index = 0;

            while (index < intsToCompress.Length)
            {
                totalValue += intsToCompress[index];
                index++;
            }

            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            // Console.WriteLine($"Total value with while loop: {totalValue}");

            totalValue = 0;

            startTime = DateTime.Now;

            index = 0;

            do
            {
                totalValue += intsToCompress[index];
                index++;
            }
            while (index < intsToCompress.Length);
            
            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            // Console.WriteLine($"Total value with do-while loop: {totalValue}");

            totalValue = 0;

            totalValue = intsToCompress.Sum();
            // Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            // Console.WriteLine($"Total value with LINQ Sum(): {totalValue}");

            totalValue = 0;

            foreach(int intForCompress in intsToCompress)
            {
                if (intForCompress > 20)
                {
                    totalValue += intForCompress;
                }
            }

            // Console.WriteLine($"Total value of ints greater than 20: {totalValue}");
        }
    }
}
