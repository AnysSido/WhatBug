using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewLocators
{
    public class ComponentViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // ViewComponent paths are hardcoded as /Components/{1}{0} in Core MVC then appended to the regular search paths.
            // This simply allows them to exist at root/Components
            if (context.ViewName.StartsWith("Components"))
                return new string[] { "/{0}" + RazorViewEngine.ViewExtension };

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // see: https://stackoverflow.com/questions/36802661/what-is-iviewlocationexpander-populatevalues-for-in-asp-net-core-mvc
            context.Values["action_displayname"] = context.ActionContext.ActionDescriptor.DisplayName;
        }
    }
}
