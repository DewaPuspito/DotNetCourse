using System;
using IntermediateProgram.Models;
using IntermediateProgram.Data;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace IntermediateProgram
{
    internal class Program2
    {
        public static void Run()
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

            string releaseDate = myComputer.ReleaseDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string price = myComputer.Price.ToString(CultureInfo.InvariantCulture);

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                CPUCores,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('"
            + myComputer.Motherboard + "', "
            + myComputer.CPUCores + ", "
            + (myComputer.HasWifi ? 1 : 0) + ", "
            + (myComputer.HasLTE ? 1 : 0) + ", '"
            + releaseDate + "', "
            + price + ", '"
            + myComputer.VideoCard + "')";

            // File.WriteAllText("log.txt", "\n" + sql + "\n");

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine("\n" + sql + "\n");

            // openFile.Close();

            // string fileText = File.ReadAllText("log.txt");

            // Console.WriteLine(fileText);

        }
    }
}
