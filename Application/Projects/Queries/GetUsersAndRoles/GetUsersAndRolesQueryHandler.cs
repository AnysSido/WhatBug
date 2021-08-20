using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    public class GetUsersAndRolesQueryHandler : IRequestHandler<GetUsersAndRolesQuery, ProjectDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersAndRolesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> Handle(GetUsersAndRolesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Projects.Where(p => p.Id == request.ProjectId);
            var dto = await _mapper.ProjectTo<ProjectDTO>(query).FirstOrDefaultAsync();
            dto.Roles = dto.Roles.GroupBy(r => r.Id).Select(r => r.First()).ToList();

            return dto;
        }
    }
}
