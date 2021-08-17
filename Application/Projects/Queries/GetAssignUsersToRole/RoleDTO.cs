using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    public class RoleDTO : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}