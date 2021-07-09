using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewModels.Priorities
{
    public class PriorityViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Color { get; set; }
        public int Order { get; set; }

        public PriorityIconViewModel PriorityIcon { get; set; }
    }
}
