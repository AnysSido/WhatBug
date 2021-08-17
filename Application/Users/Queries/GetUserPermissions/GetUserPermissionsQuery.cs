using MediatR;

namespace WhatBug.Application.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQuery : IRequest<UserPermissionsDTO>
    {
        public int UserId { get; set; }
    }
}
