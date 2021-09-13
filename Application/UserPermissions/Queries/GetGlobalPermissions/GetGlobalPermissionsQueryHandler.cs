using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.UserPermissions.Queries.GetGlobalPermissions
{
    public class GetGlobalPermissionsQueryHandler : IRequestHandler<GetGlobalPermissionsQuery, GlobalPermissionsDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetGlobalPermissionsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GlobalPermissionsDTO> Handle(GetGlobalPermissionsQuery request, CancellationToken cancellationToken)
        {
            var dto = new GlobalPermissionsDTO
            {
                Users = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync()
            };

            return dto;
        }
    }
}
