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

            services.AddDefaultIdentity<PrincipalUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddScoped<IAuthenticationProvider, IdentityAuthenticationProvider>();
            services.AddScoped<IUserClaimsPrincipalFactory<PrincipalUser>, PrincipalUserClaimsPrincipalFactory>();

            return services;
        }
    }
}
