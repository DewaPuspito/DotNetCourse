using System;
using IntermediateProgram.Models;
using IntermediateProgram.Data;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.Diagnostics;

namespace IntermediateProgram
{
    internal class Program5
    {
        public static void Run()
        {

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DapperExample dapper = new DapperExample(config);
            
            string computersJson = File.ReadAllText("ComputersSnake.json");

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destination => destination.ComputerId, 
                        options => options.MapFrom(source => source.computer_id))
                    .ForMember(destination => destination.Motherboard, 
                        options => options.MapFrom(source => source.motherboard))
                    .ForMember(destination => destination.CPUCores, 
                        options => options.MapFrom(source => source.cpu_cores))
                    .ForMember(destination => destination.HasWifi, 
                        options => options.MapFrom(source => source.has_wifi))
                    .ForMember(destination => destination.HasLTE, 
                        options => options.MapFrom(source => source.has_lte))
                    .ForMember(destination => destination.ReleaseDate, 
                        options => options.MapFrom(source => source.release_date))
                    .ForMember(destination => destination.Price, 
                        options => options.MapFrom(source => source.price))
                    .ForMember(destination => destination.VideoCard, 
                        options => options.MapFrom(source => source.video_card));
            }));

            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

            if (computersSystem != null)
            {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
                Console.WriteLine("AutoMapper : " + computerResult.Count());

                // foreach (Computer computer in computerResult)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            } 

            
            IEnumerable<Computer>? computersSystemMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            if (computersSystemMapping != null)
            {
                Console.WriteLine("JSON Property : " + computersSystemMapping.Count());
                // foreach (Computer computer in computersSystemMapping)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            } 
        }
    }
}
