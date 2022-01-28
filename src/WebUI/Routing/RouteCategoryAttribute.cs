using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Routing
{
    public enum RouteCategory
    {
        None = 0,
        Project = 1
    }

    public class RouteCategoryAttribute : ActionFilterAttribute
    {
        private RouteCategory category;

        public RouteCategoryAttribute(RouteCategory routeCategory)
        {
            category = routeCategory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["RouteCategory"] = (int)category;
        }
    }
}
