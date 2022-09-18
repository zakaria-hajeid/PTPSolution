using KoalaKit.Extensions;
using KoalaKit.Queuing.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTP.Queuing.RabbitMqService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTP.Queuing.RabbitMqService.Extinsions
{
    public static class RabitMqExtision
    {
        public static  void ConfigreRabitService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddKoalaKitCore(configuration, builder => builder.AddModules(typeof(RabbitMqModule)));

            services.AddScoped(typeof(IQuenigService<>), typeof(QueingService<>));
        }
    }
}
