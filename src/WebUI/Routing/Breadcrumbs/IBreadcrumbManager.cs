using System.Collections.Generic;
using WhatBug.WebUI.Routing.Breadcrumbs.Patterns;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public interface IBreadcrumbManager
    {
        bool TryGetBreadcrumbs(string routeName, out List<BreadcrumbPart> breadcrumbs);
    }
}
