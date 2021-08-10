using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.Common.Mapping;
using WhatBug.WebUI.Features.Projects.KanbanBoard;

namespace WhatBug.WebUI.Features.Projects.KanbanBoard
{
    public class IssueStatusGroupViewModel : IMapFrom<IssueStatusGroupDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IssueOverviewViewModel> Issues { get; set; } = new List<IssueOverviewViewModel>();
    }
}
