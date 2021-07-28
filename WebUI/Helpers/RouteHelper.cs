using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.Controllers;

namespace WhatBug.WebUI.Helpers
{
    public static class RouteHelper
    {
        public static bool IsProjectRoute(this IUrlHelper urlHelper)
        {
            return IsControllerType<ProjectsController>(urlHelper) || IsControllerType<IssuesController>(urlHelper);
        }

        public static bool IsControllerType<T>(IUrlHelper urlHelper) where T : Controller
        {
            // Check if the controller name from the route e.g. "Projects" matches the type name prefix (e.g. "Projects" from "ProjectsController")
            return urlHelper.ActionContext.RouteData.Values["controller"].ToString() == typeof(T).Name[0..^10];
        }

        public static int? GetRouteInt(this IUrlHelper urlHelper, string key)
        {
            if (urlHelper.ActionContext.RouteData.Values.TryGetValue(key, out object val))
                return Int32.Parse((string)val);

            return null;
        }
    }
}
