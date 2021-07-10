using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.PrioritySchemes;

namespace WhatBug.WebUI.ViewModels.Projects
{
    public class CreateProjectViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Priority Scheme")]
        public int SelectedPriorityScheme { get; set; }
        public List<PrioritySchemeViewModel> PrioritySchemes { get; set; } = new List<PrioritySchemeViewModel>();
        public List<SelectListItem> PrioritySchemeListItems => PrioritySchemes
            .Select(s => new SelectListItem() 
            {
                Value = s.Id.ToString(), 
                Text = s.Name 
            }).ToList();
    }
}
