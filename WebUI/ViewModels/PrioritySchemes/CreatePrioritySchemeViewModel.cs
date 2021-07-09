using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.ViewModels.PrioritySchemes
{
    public class CreatePrioritySchemeViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> PriorityIds { get; set; }

        public SelectList AllPriorities { get; set; }

        public void SetSelectList(List<PriorityViewModel> vm)
        {
            AllPriorities = new SelectList(vm.OrderBy(p => p.Order), nameof(PriorityViewModel.Id), nameof(PriorityViewModel.Name));
        }
    }
}
