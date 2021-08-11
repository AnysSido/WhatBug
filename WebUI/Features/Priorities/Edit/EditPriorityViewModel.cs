using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Queries.GetEditPriority;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Priorities.Edit
{
    public class EditPriorityViewModel : IMapFrom<EditPriorityDTO>
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [HiddenInput]
        public int IconId { get; set; }

        [Required]
        [HiddenInput]
        public int ColorId { get; set; }

        [Display(Name = "Icon")]
        public List<IconViewModel> Icons { get; set; }

        [Display(Name = "Color")]
        public List<ColorViewModel> Colors { get; set; }
    }
}
