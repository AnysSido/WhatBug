using Humanizer;

namespace WhatBug.WebUI.Routing.Breadcrumbs.Patterns
{
    public class BreadcrumbLiteralPart : BreadcrumbPart
    {
        public string Text { get; }

        internal BreadcrumbLiteralPart(string text) : base(BreadcrumbKind.Literal)
        {
            Text = text.Humanize().Transform(To.TitleCase);
        }
    }
}
