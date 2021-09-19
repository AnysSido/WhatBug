using System.Text;

namespace WhatBug.WebUI.Routing.Breadcrumbs
{
    public record Breadcrumb
    {
        public string Display { get; }
        public string Controller { get; }
        public string Action { get; }
        public bool IsReadOnly { get; private set; }

        public Breadcrumb(string display)
        {
            Display = GetDisplayString(display);
            IsReadOnly = true;
        }

        public Breadcrumb(string display, string controller, string action)
        {
            Display = GetDisplayString(display);
            Controller = controller;
            Action = action;
        }

        public void MakeReadOnly()
        {
            IsReadOnly = true;
        }

        private string GetDisplayString(string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if (sb.Length == 0)
                    sb.Append(char.ToUpper(c));
                else if (c.Equals('-'))
                    sb.Append(' ');
                else if (char.IsUpper(c))
                    sb.Append(" " + c);
                else if (char.IsWhiteSpace(sb[^1]))
                    sb.Append(char.ToUpper(c));
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
