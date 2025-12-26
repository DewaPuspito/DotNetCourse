using System;
using IntermediateProgram.Models;
using IntermediateProgram.Data;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IntermediateProgram
{
    internal class Program4
    {
        public static void Run()
        {

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DapperExample dapper = new DapperExample(config);
            
            string computersJson = File.ReadAllText("Computers.json");

            // Console.WriteLine(computersJson);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            if (computersNewtonSoft != null)
            {
                foreach (Computer comp in computersNewtonSoft)
                {
                    // Console.WriteLine(comp.Motherboard);
                    string sql = 
                    @"INSERT INTO TutorialAppSchema.Computer (
                        Motherboard,
                        CPUCores,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard
                    ) VALUES ('"
                    + EscapeSingleQuotes(comp.Motherboard)
                    + "', "
                    + (comp.CPUCores.HasValue ? comp.CPUCores.Value.ToString() : "NULL")
                    + ", " + (comp.HasWifi ? 1 : 0)
                    + ", " + (comp.HasLTE ? 1 : 0)
                    + ", " + (comp.ReleaseDate.HasValue ? $"'{comp.ReleaseDate.Value:yyyy-MM-dd HH:mm:ss}'" : "NULL")
                    + ", " + comp.Price.ToString(CultureInfo.InvariantCulture)
                    + ", '" + EscapeSingleQuotes(comp.VideoCard)
                    + "')";

                    dapper.ExecuteSql(sql);

                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            File.WriteAllText("computersCopyNewtonSoft.txt", computersCopyNewtonsoft);

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

            File.WriteAllText("computersCopySystem.txt", computersCopySystem);

        }

        static string EscapeSingleQuotes(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }
    }
}
