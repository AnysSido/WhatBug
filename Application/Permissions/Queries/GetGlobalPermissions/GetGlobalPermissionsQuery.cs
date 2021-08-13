using MediatR;

namespace WhatBug.Application.Permissions.Queries.GetGlobalPermissions
{
    public class GetGlobalPermissionsQuery :IRequest<GlobalPermissionsDTO>
    {
    }
}
