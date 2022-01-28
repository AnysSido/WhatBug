using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Fix a breaking change introduced by npgsql 6. See https://www.npgsql.org/efcore/release-notes/6.0.html
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<WhatBugDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("WhatBugDatabase")));
                //options.UseSqlite(configuration.GetConnectionString("WhatBugDatabase")));

            // We must not instantiate a new instance of WhatBugDbContext, it must be pulled from the DI container so that it is configured correctly via the AddDbContext above.
            services.AddScoped<IWhatBugDbContext>(provider => provider.GetService<WhatBugDbContext>());

            return services;
        }
    }
}
