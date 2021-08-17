using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Priorities.Queries.GetCreatePriority
{
    public class CreatePriorityDTO
    {
        public IList<IconDTO> Icons { get; set; }
        public IList<ColorDTO> Colors { get; set; }
    }
}
