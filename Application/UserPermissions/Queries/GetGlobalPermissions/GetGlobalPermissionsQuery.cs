using MediatR;

namespace WhatBug.Application.UserPermissions.Queries.GetGlobalPermissions
{
    public class GetGlobalPermissionsQuery : IRequest<GlobalPermissionsDTO>
    {
    }
}
