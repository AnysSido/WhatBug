using System.Collections.Generic;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public interface IBreadcrumbManager
    {
        bool TryGetBreadcrumbs(string controllerName, string actionName, out List<Breadcrumb> breadcrumbs);
    }
}
