using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        [Required]
        public string SelectedIconName { get; set; }

        [Required]
        public string SelectedIconColor { get; set; }

        public List<PriorityIconViewModel> AllIcons { get; set; }
    }
}
