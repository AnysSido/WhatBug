using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public class IssueTypes
    {
        private static readonly List<IssueType> _issueTypes = new List<IssueType>();

        public static readonly IssueType Task = CreateIssueType(1, "Task", Icons.CheckSquare, Colors.Blue);
        public static readonly IssueType Bug = CreateIssueType(2, "Bug", Icons.Bug, Colors.Candy);

        private static IssueType CreateIssueType(int id, string name, Icon icon, Color color)
        {
            var issueType = new IssueType { Id = id, Name = name, Icon = icon, Color = color };
            _issueTypes.Add(issueType);
            return issueType;
        }

        public static ReadOnlyCollection<IssueType> Seed()
        {
            return _issueTypes.Select(i => new IssueType { Id = i.Id, Name = i.Name, IconId = i.Icon.Id, ColorId = i.Color.Id }).ToList().AsReadOnly();
        }
    }
}
