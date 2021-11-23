using System;
using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    public class GetCommentsQueryResult
    {
        public IList<CommentDTO> Comments { get; set; }
    }

    public class CommentDTO : IMapFrom<IssueComment>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public UserDTO Author { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsByCurrentUser { get; set; }
    }

    public class UserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}