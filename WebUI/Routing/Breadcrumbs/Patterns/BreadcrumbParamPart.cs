namespace WhatBug.WebUI.Routing.Breadcrumbs.Patterns
{
    public class BreadcrumbParamPart : BreadcrumbPart
    {
        public string ParamName { get; }

        internal BreadcrumbParamPart(string paramName) : base(BreadcrumbKind.Parameter)
        {
            ParamName = paramName;
        }
    }
}
