using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetCreateProject
{
    public class PrioritySchemeDTO : IMapFrom<PriorityScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
