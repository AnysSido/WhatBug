using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public class IssueTypes
    {
        private static readonly List<IssueType> _issueTypes = new List<IssueType>();

        public static readonly IssueType Task = CreateIssueType(1, "Task", Icons.CheckSquare);
        public static readonly IssueType Bug = CreateIssueType(2, "Bug", Icons.Bug);
        public static readonly IssueType NewFeature = CreateIssueType(3, "New Feature", Icons.PlusSquare);
        public static readonly IssueType Improvement = CreateIssueType(4, "Improvement", Icons.CaretSquareUp);

        private static IssueType CreateIssueType(int id, string name, Icon icon)
        {
            var issueType = new IssueType { Id = id, Name = name, Icon = icon, IconId = icon.Id };
            _issueTypes.Add(issueType);
            return issueType;
        }

        public static ReadOnlyCollection<IssueType> Seed()
        {
            return _issueTypes.Select(i => new IssueType() { Id = i.Id, Name = i.Name, IconId = i.IconId }).ToList().AsReadOnly();
        }
    }
}
