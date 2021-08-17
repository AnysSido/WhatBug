using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;

namespace WhatBug.WebUI.ViewLocators
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        private static string _featureTag;

        public FeatureViewLocationExpander(string featureTag)
        {
            _featureTag = featureTag;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (viewLocations == null)
                throw new ArgumentNullException(nameof(viewLocations));

            var controllerDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            var featureName = controllerDescriptor?.Properties["feature"] as string;

            foreach (var location in viewLocations)
            {
                yield return location.Replace(_featureTag, featureName);
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // see: https://stackoverflow.com/questions/36802661/what-is-iviewlocationexpander-populatevalues-for-in-asp-net-core-mvc
            context.Values["action_displayname"] = context.ActionContext.ActionDescriptor.DisplayName;
        }
    }
}
