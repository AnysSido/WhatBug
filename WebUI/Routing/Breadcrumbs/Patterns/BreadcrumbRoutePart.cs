using Humanizer;
using System.Collections.Generic;

namespace WhatBug.WebUI.Routing.Breadcrumbs.Patterns
{
    public class BreadcrumbRoutePart : BreadcrumbPart
    {
        public string Text { get; }
        public string RouteName { get; }
        public IEnumerable<string> RequiredParams { get; }

        internal BreadcrumbRoutePart(string routeName, IEnumerable<string> requiredParams) : base(BreadcrumbKind.Route)
        {
            Text = routeName.Humanize().Transform(To.TitleCase);
            RouteName = routeName;
            RequiredParams = requiredParams;
        }
    }
}
