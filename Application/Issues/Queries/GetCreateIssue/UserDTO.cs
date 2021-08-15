using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class UserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}