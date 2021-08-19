﻿using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public string IconName { get; set; }
    }
}