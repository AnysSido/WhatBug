using MediatR;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    public class GetAssignUsersToRoleQuery : IRequest<AssignUsersToRolesDTO>
    {
        public int ProjectId { get; set; }
    }
}
