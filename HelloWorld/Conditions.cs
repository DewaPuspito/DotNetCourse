using System;

namespace HelloWorld
{
    internal class Conditions
    {
        public static void Run()
        {
            int myInt = 5;
            int mySecondInt = 10;

            if (myInt < mySecondInt)
            {
                myInt += 5;
            }

            // Console.WriteLine(myInt);

            string myCow = "cow";
            string myCapitalizedCow = "Cow";

            if (myCow == myCapitalizedCow)
            {
                // Console.WriteLine("The strings are equal");
            }
            else if (myCow == myCapitalizedCow.ToLower())
            {
                // Console.WriteLine("The strings are equal when ignoring case");
            }
            else
            {
                // Console.WriteLine("The strings are not equal");
            }

            switch (myCow)
            {
                case "dog":
                    // Console.WriteLine("It's a dog");
                    break;
                case "cat":
                    // Console.WriteLine("It's a cat");
                    break;
                case "cow":
                    // Console.WriteLine("It's a cow");
                    break;
                case "Cow":
                    // Console.WriteLine("It's a cow with a capital C");
                    break;
                default:
                    // Console.WriteLine("It's some other animal");
                    break;
            }
        }
    }
}