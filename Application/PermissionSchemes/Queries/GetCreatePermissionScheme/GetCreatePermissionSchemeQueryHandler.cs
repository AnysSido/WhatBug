using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WhatBug.Application.PermissionSchemes.Queries.GetCreatePermissionScheme
{
    public class GetCreatePermissionSchemeQueryHandler : IRequestHandler<GetCreatePermissionSchemeQuery, CreatePermissionSchemeDTO>
    {
        public async Task<CreatePermissionSchemeDTO> Handle(GetCreatePermissionSchemeQuery request, CancellationToken cancellationToken)
        {
            return new CreatePermissionSchemeDTO();
        }
    }
}
