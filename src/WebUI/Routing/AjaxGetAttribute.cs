using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Routing
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return Utils.IsAjaxRequest(routeContext.HttpContext.Request);
        }
    }
}