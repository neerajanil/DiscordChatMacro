using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Discord.Net;
using Discord;
using Discord.WebSocket;

namespace MacroBot
{
    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();


        public async Task MainAsync(string[] args)
        {
            
            string token = "MzU3MzY2ODMyMzI5MzkyMTI4.DJo3ag.If-hoMi7IwNbuqtBL2HCSR5LlO8"; // Remember to keep this private!
            var bot = new Bot();
            await bot.Start(token);

            BuildWebHost(args).Run();

        }


        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();
        }
    }
}
