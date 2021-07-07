using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewModels.Priorities
{
    public class CreatePriorityViewModel
    {
        public PriorityViewModel Priority { get; set; }

        [Required]
        public string SelectedIcon { get; set; }

        [Required]
        public string SelectedIconColor { get; set; }

        public List<PriorityIconViewModel> AllIcons { get; set; }
    }
}
