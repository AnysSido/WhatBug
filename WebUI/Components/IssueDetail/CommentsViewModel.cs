using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Queries.GetComments;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Components.IssueDetail
{
    public class CommentsViewModel : IMapFrom<CommentsDTO>
    {
        public IList<CommentViewModel> Comments { get; set; }
    }
}
