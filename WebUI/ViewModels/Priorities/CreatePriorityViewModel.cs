using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewModels.Priorities
{
    public class CreatePriorityViewModel
    {
        public PriorityViewModel Priority { get; set; }

        public int SelectedIcon { get; set; }
        public List<PriorityIconViewModel> AllIcons { get; set; }
    }
}
