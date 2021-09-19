using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public class BreadcrumbMiddleware
    {
        private readonly RequestDelegate _next;

        public BreadcrumbMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IBreadcrumbManager breadcrumbManager)
        {
            if (httpContext.GetEndpoint() is RouteEndpoint endpoint)
            {
                var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                var controllerName = controllerActionDescriptor.ControllerName;
                var actionName = controllerActionDescriptor.ActionName;

                if (breadcrumbManager.TryGetBreadcrumbs(controllerName, actionName, out var breadcrumbs))
                {
                    httpContext.Items["Breadcrumbs"] = breadcrumbs;
                }
            }

            await _next(httpContext);
        }
    }
}
