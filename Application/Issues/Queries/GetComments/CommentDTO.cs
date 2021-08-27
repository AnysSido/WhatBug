using System;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    public class CommentDTO : IMapFrom<IssueComment>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public UserDTO Author { get; set; }
        public DateTime Timestamp { get; set; }
    }
}