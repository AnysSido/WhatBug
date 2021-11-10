using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Projects.Queries.GetCreateProject
{
    [Authorize(Permissions.CreateProject)]
    public record GetCreateProjectQuery : IQuery<Response<GetCreateProjectQueryResult>>
    {
    }

    public class GetCreateProjectQueryHandler : IRequestHandler<GetCreateProjectQuery, Response<GetCreateProjectQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreateProjectQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetCreateProjectQueryResult>> Handle(GetCreateProjectQuery request, CancellationToken cancellationToken)
        {
            var dto = new GetCreateProjectQueryResult
            {
                PrioritySchemes = await _mapper.ProjectTo<PrioritySchemeDto>(_context.PrioritySchemes).ToListAsync(),
                PermissionSchemes = await _mapper.ProjectTo<PermissionSchemeDto>(_context.PermissionSchemes).ToListAsync()
            };

            dto.PrioritySchemes = dto.PrioritySchemes.OrderBy(s => !s.IsDefault).ThenBy(s => s.Name).ToList();
            dto.PermissionSchemes = dto.PermissionSchemes.OrderBy(s => !s.IsDefault).ThenBy(s => s.Name).ToList();

            return Response<GetCreateProjectQueryResult>.Success(dto);
        }
    }
}