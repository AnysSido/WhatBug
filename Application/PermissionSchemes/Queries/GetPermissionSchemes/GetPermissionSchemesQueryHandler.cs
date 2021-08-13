using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes
{
    public class GetPermissionSchemesQueryHandler : IRequestHandler<GetPermissionSchemesQuery, PermissionSchemesDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPermissionSchemesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PermissionSchemesDTO> Handle(GetPermissionSchemesQuery request, CancellationToken cancellationToken)
        {
            return new PermissionSchemesDTO
            {
                PermissionSchemes = await _mapper.ProjectTo<PermissionSchemeDTO>(_context.PermissionSchemes).ToListAsync()
            };
        }
    }
}
