using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class IssueStatusGroupDTO : IMapFrom<IssueStatus>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IssueOverviewDTO> Issues { get; set; } = new List<IssueOverviewDTO>();
    }
}