using Microsoft.AspNetCore.Routing;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WhatBug.WebUI.Routing.Breadcrumbs.Patterns;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public class BreadcrumbManager : IBreadcrumbManager
    {
        private ConcurrentDictionary<string, List<BreadcrumbPart>> _breadcrumbParts;

        public BreadcrumbManager(IEnumerable<EndpointDataSource> endpointSources)
        {
            var breadcrumbParts = new BreadcrumbBuilder().Build(endpointSources);

            _breadcrumbParts = new ConcurrentDictionary<string, List<BreadcrumbPart>>(breadcrumbParts);
        }

        public bool TryGetBreadcrumbs(string routeName, out List<BreadcrumbPart> breadcrumbs)
        {
            return _breadcrumbParts.TryGetValue($"{routeName}", out breadcrumbs);
        }
    }
}
