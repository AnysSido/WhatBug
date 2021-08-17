using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Projects.Queries.GetCreateProject
{
    public class CreateProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<PrioritySchemeDTO> PrioritySchemes { get; set; }
    }
}
