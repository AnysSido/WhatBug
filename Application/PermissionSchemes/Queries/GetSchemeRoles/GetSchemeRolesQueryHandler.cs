using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PermissionSchemes.Queries.GetSchemeRoles
{
    public class GetSchemeRolesQueryHandler : IRequestHandler<GetSchemeRolesQuery, SchemeDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetSchemeRolesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SchemeDTO> Handle(GetSchemeRolesQuery request, CancellationToken cancellationToken)
        {
            var dto = await _mapper.ProjectTo<SchemeDTO>(_context.PermissionSchemes).FirstOrDefaultAsync(s => s.Id == request.SchemeId);
            dto.Roles = await _mapper.ProjectTo<RoleDTO>(_context.Roles).ToListAsync();

            if (dto == null)
            {
                // TODO: Throw exception
            }

            return dto;
        }
    }
}
