using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace WhatBug.WebUI.Routing
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var request = routeContext.HttpContext.Request;

            if (request == null)
                return false;

            if (request.Headers == null)
                return false;

            if (request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return false;

            return true;
        }
    }
}
