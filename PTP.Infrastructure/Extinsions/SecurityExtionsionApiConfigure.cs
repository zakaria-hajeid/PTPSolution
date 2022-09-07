using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using PTP.Proxies.Proxies.ProxyAbstractions;

namespace PTP.Infrastructure.Extinsions
{
    public static class SecurityExtionsionApiConfigure
    {
        public static void RegisteRefitSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IUserProxy>()
                   .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["SecurityApi:ApiUri"]));

        }
    }
}
