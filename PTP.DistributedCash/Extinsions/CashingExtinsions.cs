using KoalaKit.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using KoalaKit.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koalakit.Caching.Redis;
using PTP.DistributedCash.Abstraction;
using Microsoft.Extensions.DependencyInjection;
namespace PTP.DistributedCash.Extinsions
{
    public static class CashingExtinsions
    {
        public static void ConfigreCashing(this IServiceCollection services, IConfiguration configuration)
        {
           /* services.AddKoalaKitCore(configuration, builder => builder.AddModules(typeof(KoalaRedisCacheModule)));
            services.AddScoped<ICashService, CaahService>();
           */

        }
    }
}
