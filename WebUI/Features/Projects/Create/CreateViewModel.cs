﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WhatBug.Application.Projects.Queries.GetCreateProject;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Projects.Create
{
    public class CreateViewModel : IMapFrom<CreateProjectDTO>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public int PrioritySchemeId { get; set; }
        [DisplayName("Priority Scheme")]
        public List<PrioritySchemeDTO> PrioritySchemes { get; set; } = new List<PrioritySchemeDTO>();
        public List<SelectListItem> PrioritySchemeListItems => PrioritySchemes
            .Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
    }
}
