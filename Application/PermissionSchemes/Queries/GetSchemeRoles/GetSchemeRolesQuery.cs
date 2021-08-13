using MediatR;

namespace WhatBug.Application.PermissionSchemes.Queries.GetSchemeRoles
{
    public class GetSchemeRolesQuery : IRequest<SchemeDTO>
    {
        public int SchemeId { get; set; }
    }
}
