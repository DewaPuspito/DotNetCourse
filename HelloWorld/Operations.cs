using System;

namespace HelloWorld
{
    internal class Operations
    {
        public static void Run()
        {
            int myInt = 5;
            // int mySecondInt = 10;

            // Console.WriteLine(myInt.Equals(mySecondInt));
            // Console.WriteLine(myInt.Equals(mySecondInt / 2));
            
            // Console.WriteLine(myInt != (mySecondInt));
            // Console.WriteLine(myInt == (mySecondInt/2));

            // Console.WriteLine(myInt >= (mySecondInt));
            // Console.WriteLine(myInt >= (mySecondInt / 2));
            // Console.WriteLine(myInt > (mySecondInt));
            // Console.WriteLine(myInt > (mySecondInt - 6));
            // Console.WriteLine(myInt <= (mySecondInt));
            // Console.WriteLine(myInt < (mySecondInt));

            // Console.WriteLine(5 < 10 && 5 > 10);
            // Console.WriteLine(5 < 10 || 5 > 10);

            // Console.WriteLine(myInt);

            myInt++;

            // Console.WriteLine(myInt);

            myInt += 7;

            // Console.WriteLine(myInt);

            myInt -= 8;

            // Console.WriteLine(myInt);

            // Console.WriteLine(myInt * mySecondInt);

            // Console.WriteLine(mySecondInt / myInt);

            // Console.WriteLine(mySecondInt + myInt);

            // Console.WriteLine(myInt - mySecondInt);

            // Console.WriteLine(5 + 3 * 2);

            // Console.WriteLine((5 + 3) * 2);

            // Console.WriteLine(Math.Pow(2, 5));

            // Console.WriteLine(Math.Sqrt(25));

            string myString = "test";

            // Console.WriteLine(myString);

            myString += ". second test.";

            // Console.WriteLine(myString);

            myString = myString + " \"third\\ test.";

            // Console.WriteLine(myString);

            string[] myStringArray = myString.Split(". ");

            // Console.WriteLine(myStringArray[0]);
            // Console.WriteLine(myStringArray[1]);
            // Console.WriteLine(myStringArray[2]);
        }
    }
}