using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using WhatBug.WebUI.Services.Interfaces;

namespace WhatBug.WebUI.TagHelpers
{
    public class Select2OptionTagHelper : OptionTagHelper
    {
        private readonly IIconService _iconService;

        public string Icon { get; set; }
        public string IconColor { get; set; }

        public Select2OptionTagHelper(IHtmlGenerator generator, IIconService iconService) : base(generator)
        {
            _iconService = iconService;
        }        

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagName = "option";

            var iconName = _iconService.IconNameToClass(Icon ?? string.Empty);
            var iconColor = "wb-color-" + IconColor?.ToLower();

            output.Attributes.SetAttribute("data-icon", iconName);
            output.Attributes.SetAttribute("data-icon-color", iconColor);
        }
    }
}
