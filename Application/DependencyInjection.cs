using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using WhatBug.Application.Common.Behaviors;
using WhatBug.Application.Common.Settings;
using System;
using WhatBug.Application.Authorization;
using WhatBug.Application.UserInfo;
using Microsoft.Extensions.Configuration;

namespace WhatBug.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthorizationManager, AuthorizationManager>();
            services.AddScoped<IUserInfoService, UserInfoService>();

            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddMediatR(typeof(DependencyInjection));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(NoTrackingQueryBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DemoModeBehavior<,>));

            services.AddOptions<WhatBugSettings>()
                .Configure(options =>
                {
                    options.Attachments.FileLocation = "\\userfiles\\attachments";
                    options.Attachments.AllowedExtensions = new List<string> { ".gif", ".jpg", ".jpeg", ".png", ".svg" };
                    options.Attachments.MaxFileSize = 1;
                });

            services.AddOptions().Configure<WhatBugSettings>(configuration.GetSection("WhatBug"));

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, Action<WhatBugSettings> configureOptions)
        {
            AddApplication(services, configuration);
            services.Configure(configureOptions);

            return services;
        }
    }
}
