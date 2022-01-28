using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using WhatBug.WebUI.Routing.Breadcrumbs.Patterns;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public class BreadcrumbActionFilter : IActionFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IBreadcrumbManager _breadcrumbManager;

        public BreadcrumbActionFilter(IUrlHelperFactory urlHelperFactory, IBreadcrumbManager breadcrumbManager)
        {
            _urlHelperFactory = urlHelperFactory;
            _breadcrumbManager = breadcrumbManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(context);

            if (context.HttpContext.GetEndpoint() is RouteEndpoint endpoint)
            {
                var routeName = endpoint?.Metadata.GetMetadata<RouteNameMetadata>()?.RouteName;
                var routeValues = context.HttpContext.Request.RouteValues;

                if (routeName != null && _breadcrumbManager.TryGetBreadcrumbs(routeName, out var breadcrumbSegments))
                {
                    var breadcrumbs = new List<Breadcrumb>();

                    foreach (var breadcrumbPart in breadcrumbSegments)
                    {
                        if (breadcrumbPart.IsLiteral)
                        {
                            breadcrumbs.Add(new Breadcrumb(((BreadcrumbLiteralPart)breadcrumbPart).Text));
                        }
                        else if (breadcrumbPart.IsRoute)
                        {
                            var part = (BreadcrumbRoutePart)breadcrumbPart;

                            var requiredParamValues = routeValues
                                .Where(rv => part.RequiredParams.Contains(rv.Key));

                            var url = urlHelper.RouteUrl(part.RouteName, requiredParamValues);

                            if (url != null)
                                breadcrumbs.Add(new Breadcrumb(url, part.Text));
                        }
                        else if (breadcrumbPart.IsParameter)
                        {
                            /*
                             *  TODO: Implement parameter replacement
                             *  e.g. if URI is /whatbug/projects/1/users then
                             *  /1/ can be displayed in breadcrumbs with a replaced value (project key for example).
                             */
                        }
                    }

                    if (breadcrumbs.Count > 0)
                        context.HttpContext.Items["Breadcrumbs"] = breadcrumbs;
                }
            }

        }
    }
}
