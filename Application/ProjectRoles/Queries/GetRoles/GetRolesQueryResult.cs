using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.ProjectRoles.Queries.GetRoles
{
    public class GetRolesQueryResult
    {
        public IList<RoleDto> Roles { get; set; }
    }

    public class RoleDto : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}