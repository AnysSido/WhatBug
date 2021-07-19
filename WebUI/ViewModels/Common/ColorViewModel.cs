using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewModels.Common
{
    public class ColorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LowerName => Name.ToLower();
    }
}
