using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Entities
{
    public class IssueComment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public string IssueId { get; set; }
        public Issue Issue { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
