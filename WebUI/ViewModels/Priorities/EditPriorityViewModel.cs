using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Common;

namespace WhatBug.WebUI.ViewModels.Priorities
{
    public class EditPriorityViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ColorIconViewModel ColorIcon { get; set; }

        [Required]
        [HiddenInput]
        public int SelectedIcon { get; set; }

        [Required]
        [HiddenInput]
        public int SelectedColor { get; set; }

        [Display(Name = "Icon")]
        public List<IconViewModel> AllIcons { get; set; }

        [Display(Name = "Color")]
        public List<ColorViewModel> AllColors { get; set; }
    }
}
