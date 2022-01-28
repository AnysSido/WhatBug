using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public class IssueStatuses
    {
        private static readonly List<IssueStatus> _issueStatuses = new List<IssueStatus>();

        public static IssueStatus Backlog = CreateIssueStatus(1, "Backlog");
        public static IssueStatus ToDo = CreateIssueStatus(2, "To Do");
        public static IssueStatus InProgress = CreateIssueStatus(3, "In Progress");
        public static IssueStatus Done = CreateIssueStatus(4, "Done");

        private static IssueStatus CreateIssueStatus(int id, string name)
        {
            var issueStatus = new IssueStatus { Id = id, Name = name };
            _issueStatuses.Add(issueStatus);
            return issueStatus;
        }

        public static ReadOnlyCollection<IssueStatus> Seed()
        {
            return _issueStatuses.AsReadOnly();
        }
    }
}
