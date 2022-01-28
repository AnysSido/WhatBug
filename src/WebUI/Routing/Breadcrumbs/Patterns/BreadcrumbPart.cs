namespace WhatBug.WebUI.Routing.Breadcrumbs.Patterns
{
    public enum BreadcrumbKind { Literal, Parameter, Route }

    public abstract class BreadcrumbPart
    {
        public BreadcrumbKind PartKind { get; }

        private protected BreadcrumbPart(BreadcrumbKind partKind)
        {
            PartKind = partKind;
        }

        public bool IsLiteral => PartKind == BreadcrumbKind.Literal;

        public bool IsRoute => PartKind == BreadcrumbKind.Route;

        public bool IsParameter => PartKind == BreadcrumbKind.Parameter;
    }
}
