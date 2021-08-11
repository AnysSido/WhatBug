using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Behaviors;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Services;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPermissionSchemeService, PermissionSchemeService>();
            services.AddScoped<IGlobalPermissionService, GlobalPermissionService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddMediatR(typeof(DependencyInjection));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
