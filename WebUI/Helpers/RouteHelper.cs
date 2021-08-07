using Microsoft.AspNetCore.Mvc;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Helpers
{
    public static class RouteHelper
    {
        public static RouteCategory GetRouteCategory(this IUrlHelper urlHelper)
        {
            if (urlHelper.ActionContext.HttpContext.Items.TryGetValue("RouteCategory", out var category))
            {
                return (RouteCategory)category;
            }
            return RouteCategory.None;
        }

        public static bool TryGetRouteInt(this IUrlHelper urlHelper, string key, out int value)
        {
            if (urlHelper.ActionContext.RouteData.Values.TryGetValue(key, out object val))
            {
                if (int.TryParse((string)val, out int result))
                {
                    value = result;
                    return true;
                }
            }
            
            value = 0;
            return false;
        }
    }
}
