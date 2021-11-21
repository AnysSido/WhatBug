namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public class Breadcrumb
    {
        public string Url { get; }
        public string Text { get; }

        public Breadcrumb(string url, string text)
        {
            Url = url;
            Text = text;
        }

        public Breadcrumb(string text)
        {
            Text = text;
        }

        public bool IsUri => Url != null;
    }
}
