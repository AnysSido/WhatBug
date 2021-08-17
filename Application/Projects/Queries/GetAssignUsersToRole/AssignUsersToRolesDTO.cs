using System.Collections.Generic;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    public class AssignUsersToRolesDTO
    {
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> UserIds { get; set; }
        public ProjectDTO Project { get; set; }
        public IList<UserDTO> Users { get; set; }
        public IList<RoleDTO> Roles { get; set; }
    }
}
