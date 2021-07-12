﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.PrioritySchemes;

namespace WhatBug.Application.DTOs.Projects
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PrioritySchemeDTO PriorityScheme { get; set; }
    }
}
