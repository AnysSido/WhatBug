using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    public class GetAssignUsersToRoleQueryResult
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<UserDTO> UsersInRole { get; set; }
        public IList<UserDTO> AvailableUsers { get; set; }
    }

    public class UserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
