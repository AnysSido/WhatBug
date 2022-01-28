using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace WhatBug.WebUI.TagHelpers
{
    [HtmlTargetElement("icon", TagStructure = TagStructure.WithoutEndTag)]
    public class IconTagHelper : TagHelper
    {
        public string Icon { get; set; } // WhatBug icon name
        public string Color { get; set; } // WhatBug color

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "i";
            output.TagMode = TagMode.StartTagAndEndTag;

            var iconColor = "wb-color-" + Color?.ToLower();

            // TODO: Remove space stripping once all code using icons is updated
            var iconName = Regex.Replace(Icon, @"\s+", "");

            output.AddClass(iconColor, HtmlEncoder.Default);
            output.AddClass("icon", HtmlEncoder.Default);
            output.AddClass($"icon--{iconName}", HtmlEncoder.Default);
        }
    }
}