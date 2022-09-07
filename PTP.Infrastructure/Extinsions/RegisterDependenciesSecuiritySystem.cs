using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTP.Core.Repositores;
using PTP.Data.SQL;
using PTP.Data.SQL.Repositories;
using PTP.Infrastructure.AutoMapper;
using PTP.Infrastructure.Middlwars;
using PTP.Infrastructure.Services;
using Security.Application.Repository;
using Security.Application.Repository.ConcreteRepository;
using Security.Core.Context;
using Security.Core.Entities;
using Security.percestance;
using Security.percestance.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Infrastructure.Extinsions
{
    public static class RegisterDependenciesSecuiritySystem
    {
        public static void RegisterServicesSecurity(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<DbContext, DataContext>();
            services.AddScoped<DataContext>();
            services.AddScoped<IUserServices, userServices>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUnitOfWork<DataContext>, UnitOfWork<DataContext>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MapperProfile));

            services.AddDbContext<DataContext>(x =>
x.UseSqlServer(configuration.GetConnectionString("DbContextEntities"), b => b.MigrationsAssembly("Security.Core")));
            IdentityBuilder builder = services.AddIdentityCore<Users>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;//@ or space etc...
                opt.Password.RequireUppercase = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<Users>>();

            services.AddIdentityCore<Users>(o =>
            {
                o.Stores.MaxLengthForKeys = 128;
                o.SignIn.RequireConfirmedAccount = true;
            }).AddDefaultTokenProviders();


            services.AddIdentity<Users, Role>()
     .AddEntityFrameworkStores<DataContext>()
     .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {

                options.SignIn.RequireConfirmedAccount = false;


            });
        }

        public static void ConfigServiceseExtinsionsSecurity(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(TransactionMiddileware));
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));


        }
    }
}
