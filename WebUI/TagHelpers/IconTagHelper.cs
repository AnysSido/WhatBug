using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using WhatBug.WebUI.Services.Interfaces;

namespace WhatBug.WebUI.TagHelpers
{
    public class IconTagHelper : TagHelper
    {
        private readonly IIconService _iconService;

        public string Icon { get; set; } // WhatBug icon name
        public string Color { get; set; } // WhatBug color

        public IconTagHelper(IIconService iconService)
        {
            _iconService = iconService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "i";

            var iconName = _iconService.IconNameToClass(Icon ?? string.Empty);
            var iconColor = "wb-color-" + Color?.ToLower();

            output.AddClass(iconColor, HtmlEncoder.Default);

            foreach (var @class in iconName.Split())
            {
                output.AddClass(@class, HtmlEncoder.Default);
            }
        }
    }
}
