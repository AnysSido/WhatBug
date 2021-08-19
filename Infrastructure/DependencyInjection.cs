using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Infrastructure.Identity;

namespace WhatBug.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityDatabase")));

            services.AddDefaultIdentity<PrincipalUser>(options => 
            { 
                // TODO: Match these with application
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddClaimsPrincipalFactory<PrincipalUserClaimsPrincipalFactory>();

            services.AddScoped<IAuthenticationProvider, IdentityAuthenticationProvider>();

            services.AddAutoMapper(typeof(DependencyInjection));

            return services;
        }
    }
}
