using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PTP.Queing.Handlers.CreateUserHandlers;
using PTP.Queing.Meassage;
using PTP.Queuing.RabbitMqService.Extinsions;
using System;
using System.IO;
using System.Reflection;

namespace PTP.RabbitMqServices
{
    internal class Program
    {
        static void Main(string[] args)
        {
                    var configuration = new ConfigurationBuilder()
                      .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                      .AddJsonFile("appsettings.json", true, true)
                      .Build();


            var host = CreateHostBuilder(args).Build();
            host.UseQueuingRabbitMqConsumers(
             typeof(CreateUserMessage)
             );
            host.Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureAppConfiguration((config) =>
    {
        config.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigreRabitService(hostContext.Configuration);

        services.ConfigreRabitHandlersServices();
    });


    }
}
