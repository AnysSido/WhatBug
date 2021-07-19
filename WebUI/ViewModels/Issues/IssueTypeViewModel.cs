using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Common;

namespace WhatBug.WebUI.ViewModels.Issues
{
    public class IssueTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ColorIconViewModel ColorIcon { get; set; }
    }
}
