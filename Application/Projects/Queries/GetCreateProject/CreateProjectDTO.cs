using System.Collections.Generic;

namespace WhatBug.Application.Projects.Queries.GetCreateProject
{
    public class CreateProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public IList<PrioritySchemeDTO> PrioritySchemes { get; set; }
    }
}
