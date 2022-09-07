using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTP.Core.Common;
using PTP.Core.Entitys;
using PTP.Core.Repositores;
using PTP.Core.Servecis;
using PTP.Core.Services;
using PTP.Core.Specification;
using PTP.Data.SQL;
using PTP.Data.SQL.Repositories;
using PTP.Infrastructure.AutoMapper;
using PTP.Infrastructure.EntitySpecification;
using PTP.Infrastructure.Middlwars;
using PTP.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Infrastructure.Extinsions
{
    public static class RegisterDependencies
    {

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbContext, DbContextTest>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICleintServices, CleintServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<DbContext, DbContextTest>();
            services.AddSingleton<ICleintContext, CleintContext>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IServices, PTP.Infrastructure.Services.Services>();
            services.AddScoped(typeof(IRepositories<,>), typeof(Repository<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
           services.AddAutoMapper(typeof(MapperProfile));
            services.AddScoped(typeof(Specification<Cleint>),typeof(ClientSpecefication));

            services.AddScoped(typeof(IServices<,>), typeof(Service<,>));
            services.AddDbContext<DbContextTest> (options => options.UseSqlServer(configuration.GetConnectionString("DbContextTestEntities")).EnableSensitiveDataLogging());

        }

        public static void ConfigServiceseExtinsions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "PTP API");
                c.RoutePrefix = "";
            });
        }

    }


}
