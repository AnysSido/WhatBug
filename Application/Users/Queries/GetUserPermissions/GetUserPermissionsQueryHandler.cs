using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, UserPermissionsDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetUserPermissionsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserPermissionsDTO> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var dto = await _mapper.ProjectTo<UserPermissionsDTO>(_context.Users.Where(u => u.Id == request.UserId)).FirstOrDefaultAsync();
            return dto;
        }
    }
}
