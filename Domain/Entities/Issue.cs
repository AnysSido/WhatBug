using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Entities
{
    public class Issue
    {
        public int IssueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
