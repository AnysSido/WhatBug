using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public class BreadcrumbManager : IBreadcrumbManager
    {
        private readonly ConcurrentDictionary<string, List<Breadcrumb>> _breadcrumbs;

        public BreadcrumbManager(IEnumerable<EndpointDataSource> endpointSources)
        {
            _breadcrumbs = new ConcurrentDictionary<string, List<Breadcrumb>>();
            Init(endpointSources);
        }

        private void Init(IEnumerable<EndpointDataSource> endpointSources)
        {
            var assembly = typeof(BreadcrumbManager).Assembly;
            var endpoints = endpointSources.SelectMany(es => es.Endpoints).OfType<RouteEndpoint>();
            var controllers = assembly.GetTypes().Where(t => typeof(Controller).IsAssignableFrom(t)).ToDictionary(k => k.Name.ToLowerInvariant(), v => v);

            foreach (var endpoint in endpoints)
            {
                var httpMethod = endpoint.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods?[0];
                if (httpMethod != null && httpMethod != "GET")
                    continue;

                var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                if (controllerActionDescriptor == null)
                    continue;

                var controllerName = controllerActionDescriptor.ControllerName;
                var actionName = controllerActionDescriptor.ActionName;
                var breadcrumbs = new List<Breadcrumb> { new Breadcrumb("Home", "Home", "Index") };

                foreach (var segment in endpoint.RoutePattern.PathSegments.Where(s => s.IsSimple).Select(p => p.Parts[0]))
                {
                    if (segment.IsParameter)
                    {
                        var part = segment as RoutePatternParameterPart;

                        if (part.Name.Equals("controller", StringComparison.OrdinalIgnoreCase))
                        {
                            var controller = controllers[$"{controllerName}Controller".ToLowerInvariant()];
                            if (controller?.GetMethod("Index") != null)
                            {
                                breadcrumbs.Add(new Breadcrumb(controllerName, controllerName, "Index"));
                            }
                        }
                        else if (part.Name.Equals("action", StringComparison.OrdinalIgnoreCase))
                        {
                            if (part.Default == null)
                                breadcrumbs.Add(new Breadcrumb(actionName));
                        }

                    }
                    else if (segment.IsLiteral)
                    {
                        var part = segment as RoutePatternLiteralPart;

                        if (controllers.TryGetValue($"{part.Content}Controller".ToLowerInvariant(), out var controller))
                        {
                            if (controller.GetMethod("Index") != null)
                                breadcrumbs.Add(new Breadcrumb(part.Content, part.Content, "Index"));
                        }
                        else
                        {
                            breadcrumbs.Add(new Breadcrumb(part.Content));
                        }
                    }
                }

                if (breadcrumbs.Last() == breadcrumbs[^2])
                    breadcrumbs.RemoveAt(breadcrumbs.Count - 1);

                breadcrumbs.Last().MakeReadOnly();

                _breadcrumbs[$"{controllerName}.{actionName}"] = breadcrumbs;
            }
        }

        public bool TryGetBreadcrumbs(string controllerName, string actionName, out List<Breadcrumb> breadcrumbs)
        {
            return _breadcrumbs.TryGetValue($"{controllerName}.{actionName}", out breadcrumbs);
        }
    }
}
