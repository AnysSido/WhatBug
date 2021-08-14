using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WhatBug.WebUI.ViewLocators
{
    public static class ServiceCollectionExtensions
    {
        private static string _featureFolder = "Features";
        private static string _featureTag = "{Feature}";
        private static string _partialDirTag = "{PartialDir}";
        private static string _partialViewTag = "{PartialView}";

    public static IMvcBuilder AddFeatureFolders(this IMvcBuilder services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));            

            services.AddMvcOptions(o =>
            {
                o.Conventions.Add(new FeatureControllerModelConvention(_featureFolder));
                o.Conventions.Add(new PartialActionModelConvention());
            })                 
            .AddRazorOptions(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add($"{_featureTag}\\{{0}}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add($"{_featureTag}\\{{0}}\\{{0}}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add($"{_featureTag}\\{_partialDirTag}\\{_partialViewTag}" + RazorViewEngine.ViewExtension);

                // Support default style
                o.ViewLocationFormats.Add(@"\Shared\{0}" + RazorViewEngine.ViewExtension);

                o.ViewLocationExpanders.Add(new FeatureViewLocationExpander(_featureTag));
                o.ViewLocationExpanders.Add(new PartialViewLocationExpander(_partialDirTag, _partialViewTag));
            });

            return services;
        }
    }
}
