using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Helpers
{
    /*
     * Add script attribute "on-content-loaded" to wrap script contents in a javascript function that executes
     * when page has loaded. Used for inline ViewComponent scripts that ordinarily try to load before
     * scripts for the page have loaded (e.g. jQuery).
     * See: https://stackoverflow.com/questions/44511334/javascript-in-a-view-component
     */
    [HtmlTargetElement("script", Attributes = "on-content-loaded")]
    public class ScriptTagHelper : TagHelper
    {
        /// <summary>
        /// Execute script only once document is loaded.
        /// </summary>
        public bool OnContentLoaded { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!OnContentLoaded)
            {
                base.Process(context, output);
            }
            else
            {
                var content = output.GetChildContentAsync().Result;
                var javascript = content.GetContent();

                var sb = new StringBuilder();
                sb.Append("document.addEventListener('DOMContentLoaded',");
                sb.Append("function() {");
                sb.Append(javascript);
                sb.Append("});");

                output.Content.SetHtmlContent(sb.ToString());
            }
        }
    }
}
