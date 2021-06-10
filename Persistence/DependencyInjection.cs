using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WhatBugDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WhatBugDatabase")));

            // We must not instantiate a new instance of WhatBugDbContext, it must be pulled from the DI container so that it is configured correctly via the AddDbContext above.
            services.AddScoped<IWhatBugDbContext>(provider => provider.GetService<WhatBugDbContext>());

            return services;
        }
    }
}
