using System;

namespace HelloWorld
{
    internal class DataTypes
    {
        public static void Run()
        {
            // Array 

            // string[] firstGroceryArray = new string[2];
            string[] firstGroceryArray = ["Apples", "Bananas"];
            firstGroceryArray[1] = "Oranges";

            // Console.WriteLine(firstGroceryArray[0]);
            // Console.WriteLine(firstGroceryArray[1]);

            // List

            // List<string> firstGunList = ["Pistol", "Rifle"];
            List<string> firstGunList = new List<string>();

            firstGunList.Add("Pistol");
            firstGunList.Add("Rifle");

            // Console.WriteLine(firstGunList[0]);
            // Console.WriteLine(firstGunList[1]);

            IEnumerable<string> firstGunEnumerable = firstGunList;

            List<string> secondGunList = firstGunEnumerable.ToList();

            int[,,] myMultiDimensionalArray = {
                {
                {1, 2, 3 },
                {4, 5, 6 },
                {7, 8, 9 }
                },
                {
                {10, 11, 12 },
                {13, 14, 15 },
                {16, 17, 18 }
                },
                {
                {19, 20, 21 },
                {22, 23, 24 },
                {25, 26, 27 }
                }
            };

            // Console.WriteLine(myMultiDimensionalArray[0, 0, 1]);
            // Console.WriteLine(myMultiDimensionalArray[1, 2, 1]);

            // Dictionary

            Dictionary<string, int> groceryPrices = new Dictionary<string, int>();

            groceryPrices["Apple"] = 2;
            groceryPrices["Banana"] = 1;

            // Console.WriteLine(groceryPrices["Apple"]);
            // Console.WriteLine(groceryPrices["Banana"]);
            // Console.WriteLine(groceryPrices["Orange"]);
        }
    }
}