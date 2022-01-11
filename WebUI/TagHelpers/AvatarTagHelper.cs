using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.WebUI.TagHelpers
{
    [HtmlTargetElement("avatar", TagStructure =  TagStructure.WithoutEndTag)]
    public class AvatarTagHelper : TagHelper
    {
        public string Email { get; set; }
        public int Size { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.StartTagOnly;

            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(Email.Trim().ToLowerInvariant()));
                var hash = BitConverter.ToString(result).Replace("-", "").ToLower();
                var size = Size != default ? $"&s={Size}" : string.Empty;
                var url = $"https://gravatar.com/avatar/{hash}?d=identicon{size}";

                output.Attributes.SetAttribute("src", url);
                output.Attributes.SetAttribute("alt", "User Profile Picture");
            };
        }
    }
}
