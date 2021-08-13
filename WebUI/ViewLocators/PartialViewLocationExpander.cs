using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;

namespace WhatBug.WebUI.ViewLocators
{
    public class PartialViewLocationExpander : IViewLocationExpander
    {
        private string _partialDirTag;
        private string _partialViewTag;

        public PartialViewLocationExpander(string partialDirTag, string partialViewTag)
        {
            _partialDirTag = partialDirTag;
            _partialViewTag = partialViewTag;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (viewLocations == null)
                throw new ArgumentNullException(nameof(viewLocations));

            var actionDescriptor = context.ActionContext.ActionDescriptor;
            var partialDir = actionDescriptor?.Properties["partialdir"] as string;
            var partialView = actionDescriptor?.Properties["partialview"] as string;

            foreach (var location in viewLocations)
            {
                yield return location.Replace(_partialDirTag, partialDir).Replace(_partialViewTag, partialView);
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // see: https://stackoverflow.com/questions/36802661/what-is-iviewlocationexpander-populatevalues-for-in-asp-net-core-mvc
            context.Values["action_displayname"] = context.ActionContext.ActionDescriptor.DisplayName;
        }
    }
}
