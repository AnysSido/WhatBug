using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using WhatBug.WebUI.Routing.Breadcrumbs.Patterns;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public class BreadcrumbBuilder
    {
        private Dictionary<string, Type> _controllerTypes;
        private IEnumerable<RouteEndpoint> _endpoints;

        public Dictionary<string, List<BreadcrumbPart>> Build(IEnumerable<EndpointDataSource> endpointSources)
        {
            _controllerTypes = typeof(BreadcrumbBuilder).Assembly.GetTypes()
                .Where(t => typeof(Controller).IsAssignableFrom(t))
                .ToDictionary(k => k.Name, v => v);

            _endpoints = endpointSources
                .SelectMany(e => e.Endpoints)
                .OfType<RouteEndpoint>();

            var _breadcrumbs = new Dictionary<string, List<BreadcrumbPart>>();

            foreach (var endpoint in _endpoints)
            {
                if (!IsGetEndpoint(endpoint))
                    continue;

                var endpointName = GetEndpointName(endpoint);

                if (endpointName == null)
                    continue;

                _breadcrumbs[endpointName] = new List<BreadcrumbPart> { new BreadcrumbRoutePart("Home", new List<string>()) };
                var segments = ExtractEndpointSegments(endpoint);

                foreach (var segment in segments)
                {
                    if (segment.IsLiteral)
                    {
                        if (segments.Last() == segment)
                        {
                            _breadcrumbs[endpointName].Add(new BreadcrumbLiteralPart(((RoutePatternLiteralPart)segment).Content));
                        }
                        else
                        {
                            var controller = GetControllerForSegment((RoutePatternLiteralPart)segment);

                            if (controller?.GetMethod("Index") != null)
                            {
                                var segmentEndpoint = GetEndpoint(controller.Name, "Index");

                                if (segmentEndpoint != null)
                                {
                                    var segmentEndpointName = GetEndpointName(segmentEndpoint);
                                    var endpointParams = segmentEndpoint.RoutePattern.Parameters.Select(p => p.Name).ToList();

                                    _breadcrumbs[endpointName].Add(new BreadcrumbRoutePart(segmentEndpointName, endpointParams));
                                }
                            }
                        }
                    }
                    else if (segment.IsParameter)
                    {
                        var name = ((RoutePatternParameterPart)segment).Name;
                        _breadcrumbs[endpointName].Add(new BreadcrumbParamPart(name));
                    }
                }
            }

            _breadcrumbs["Home"] = new List<BreadcrumbPart> { new BreadcrumbLiteralPart("Home")};

            return _breadcrumbs;
        }

        private bool IsGetEndpoint(RouteEndpoint endpoint) =>
            endpoint?.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods?[0] == "GET";

        private string GetEndpointName(RouteEndpoint endpoint) =>
            endpoint?.Metadata.GetMetadata<RouteNameMetadata>()?.RouteName;

        private IEnumerable<RoutePatternPart> ExtractEndpointSegments(RouteEndpoint endpoint) =>
            endpoint.RoutePattern.PathSegments.Where(s => s.IsSimple).Select(p => p.Parts[0]);

        private RouteEndpoint GetEndpoint(string controllerName, string actionName) =>
            _endpoints
                .Where(e => e.Metadata.GetMetadata<ControllerActionDescriptor>()?.ControllerTypeInfo.Name == controllerName)
                .Where(e => e.Metadata.GetMetadata<ControllerActionDescriptor>().ActionName == actionName)
                .FirstOrDefault();

        private Type GetControllerForSegment(RoutePatternLiteralPart segment)
        {
            var controllerName = $"{segment.Content.Dehumanize()}Controller";
            if (_controllerTypes.TryGetValue(controllerName, out var controllerType))
                return controllerType;

            return null;
        }
    }
}
