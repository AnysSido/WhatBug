﻿using System;
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

        public static readonly IssueType Task = CreateIssueType(1, "Task", ColorIcons.BlueTick);
        public static readonly IssueType Bug = CreateIssueType(2, "Bug", ColorIcons.RedBug);

        private static IssueType CreateIssueType(int id, string name, ColorIcon colorIcon)
        {
            var issueType = new IssueType { Id = id, Name = name,  ColorIcon = colorIcon };
            _issueTypes.Add(issueType);
            return issueType;
        }

        public static ReadOnlyCollection<IssueType> Seed()
        {
            return _issueTypes.Select(i => new IssueType { Id = i.Id, Name = i.Name, ColorIconId = i.ColorIcon.Id }).ToList().AsReadOnly();
        }
    }
}
