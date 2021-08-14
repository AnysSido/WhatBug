using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WhatBug.Application.Common.Behaviors;
using WhatBug.Application.Services;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGlobalPermissionService, GlobalPermissionService>();
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddMediatR(typeof(DependencyInjection));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
