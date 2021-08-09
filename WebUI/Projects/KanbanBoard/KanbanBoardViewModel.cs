using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Projects.KanbanBoard
{
    public class KanbanBoardViewModel : IMapFrom<KanbanBoardDTO>
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public IList<IssueStatusGroupViewModel> IssueStatusGroups { get; set; } = new List<IssueStatusGroupViewModel>();
    }
}
