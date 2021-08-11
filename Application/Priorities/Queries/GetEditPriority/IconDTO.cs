using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public class IconDTO : IMapFrom<Icon>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
