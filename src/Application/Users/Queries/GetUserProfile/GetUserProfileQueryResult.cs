using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryResult
    {
        public string FullName { get; set; }
        public string Email { get; set; }
       public IList<IssueDTO> RecentIssues { get; set; }
        public IList<IssueCommentDTO> RecentComments { get; set; }
    }

    public class IssueDTO
    {
        public string Id { get; set; }
        public int ProjectId { get; set; }
        public string Summary { get; set; }
        public int AssigneeId { get; set; }
        public string AssigneeFullName { get; set; }
        public string AssigneeEmail { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }

    public class IssueCommentDTO
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public string IssueId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
    }
}
