using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.ViewModels.PrioritySchemes
{
    public class EditPrioritySchemeViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> SelectedPriorityIds { get; set; }
        public List<PriorityViewModel> AllPriorities { get; set; } = new List<PriorityViewModel>();

        public IEnumerable<SelectListItem> PriorityListItems => AllPriorities.Select(p =>
            new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
    }
}
