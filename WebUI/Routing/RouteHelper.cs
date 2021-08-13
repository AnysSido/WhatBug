using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WhatBug.WebUI.Routing
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

        public static bool TryGetInt(this IUrlHelper urlHelper, string key, out int value)
        {
            if (TryGetRouteInt(urlHelper, key, out value))
                return true;

            if (TryGetQueryInt(urlHelper, key, out value))
                return true;

            value = default(int);
            return false;
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

            value = default(int);
            return false;
        }

        public static bool TryGetQueryInt(this IUrlHelper urlHelper, string key, out int value)
        {
            if (urlHelper.ActionContext.HttpContext.Request.Query.TryGetValue(key, out var val))
            {
                if (int.TryParse((string)val, out int result))
                {
                    value = result;
                    return true;
                }
            }
            value = default(int);
            return false;
        }
    }
}
