using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    public class GetUsersAndRolesQueryHandler : IRequestHandler<GetUsersAndRolesQuery, ProjectDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthenticationProvider _authProvider;
        private readonly IMapper _mapper;

        public GetUsersAndRolesQueryHandler(IWhatBugDbContext context, IMapper mapper, IAuthenticationProvider authProvider)
        {
            _context = context;
            _mapper = mapper;
            _authProvider = authProvider;
        }

        public async Task<ProjectDTO> Handle(GetUsersAndRolesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Projects.Where(p => p.Id == request.ProjectId);
            var dto = await _mapper.ProjectTo<ProjectDTO>(query).FirstOrDefaultAsync();
            dto.Roles = dto.Roles.GroupBy(r => r.Id).Select(r => r.First()).ToList();

            // TODO: Fix this. We should not be querying usernames one by one. This should be replaced with data
            // from the User table but right now the User table is empty so we are using username from the
            // authentication table.
            foreach (var user in dto.Roles.SelectMany(r => r.Users).Select(r => r).ToList())
            {
                user.Username = await _authProvider.GetUsername(user.Id);
            }

            return dto;
        }
    }
}
