using System;
using IntermediateProgram.Models;
using IntermediateProgram.Data;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace IntermediateProgram
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            DapperExample dapper = new DapperExample(config);
            EntityFrameworkExample entityFramework = new EntityFrameworkExample(config);

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            DateTime rightnow = dapper.LoadDataSingle<DateTime>( "SELECT GETDATE()");

            Computer myComputer = new Computer()
            {
                Motherboard = "ASUS ROG STRIX B550-F",
                CPUCores = 8,
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 1299.99m,
                VideoCard = "NVIDIA GeForce RTX 3070"
            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges(); 

            string releaseDate = myComputer.ReleaseDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string price = myComputer.Price.ToString(CultureInfo.InvariantCulture);

            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     CPUCores,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('"
            // + myComputer.Motherboard + "', "
            // + myComputer.CPUCores + ", "
            // + (myComputer.HasWifi ? 1 : 0) + ", "
            // + (myComputer.HasLTE ? 1 : 0) + ", '"
            // + releaseDate + "', "
            // + price + ", '"
            // + myComputer.VideoCard + "');";

            // bool result = dapper.ExecuteSql(sql);

            // Console.WriteLine($"{result} row(s) inserted");

            string select = "SELECT * FROM TutorialAppSchema.Computer;";

            // Dapper version

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(select).ToList();

            // Console.WriteLine("'ComputerId', 'Motherboard', 'CPU Cores', 'Has Wifi', 'Has LTE', 'Release Date', 'Price', 'Video Card'");
            // foreach(Computer singleComputer in computers) 
            {
                // Console.WriteLine($"ComputerId: {singleComputer.ComputerId}" +
                //                   $"Motherboard: {singleComputer.Motherboard}" +
                //                   $", CPU Cores: {singleComputer.CPUCores}" +
                //                   $", Has Wifi: {singleComputer.HasWifi}" +
                //                   $", Has LTE: {singleComputer.HasLTE}" +
                //                   $", Release Date: {singleComputer.ReleaseDate}" +
                //                   $", Price: {singleComputer.Price}" +
                //                   $", Video Card: {singleComputer.VideoCard}");
            }

            // Entity Framework version

            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();

            if (computersEf != null) 
            {
                // Console.WriteLine("'ComputerId', 'Motherboard', 'CPU Cores', 'Has Wifi', 'Has LTE', 'Release Date', 'Price', 'Video Card'");
                // foreach(Computer singleComputer in computersEf) 
                // {
                //     Console.WriteLine($"ComputerId: {singleComputer.ComputerId}" + 
                //                     $"Motherboard: {singleComputer.Motherboard}" +
                //                     $", CPU Cores: {singleComputer.CPUCores}" +
                //                     $", Has Wifi: {singleComputer.HasWifi}" +
                //                     $", Has LTE: {singleComputer.HasLTE}" +
                //                     $", Release Date: {singleComputer.ReleaseDate}" +
                //                     $", Price: {singleComputer.Price}" +
                //                     $", Video Card: {singleComputer.VideoCard}");
                // }
            }

            //     Console.WriteLine($"Motherboard: {myComputer.Motherboard}");
            //     Console.WriteLine($"CPU Core: {myComputer.CPUCore}");
            //     Console.WriteLine($"Has Wifi: {myComputer.HasWifi}");
            //     Console.WriteLine($"Has LTE: {myComputer.HasLTE}");
            //     Console.WriteLine($"Release Date: {myComputer.ReleaseDate}");
            //     Console.WriteLine($"Price: {myComputer.Price}");
            //     Console.WriteLine($"Video Card: {myComputer.VideoCard}");

            // Program2.Run();
            // Program3.Run();
            // Program4.Run();
            // Program5.Run();
            await Program6.Run();
        }
    }
}
