using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Navigation.Queries.GetProjectNavigation
{
    public class GetProjectNavigationQueryHandler : IRequestHandler<GetProjectNavigationQuery, ProjectNavigationDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectNavigationQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectNavigationDTO> Handle(GetProjectNavigationQuery request, CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<ProjectNavigationDTO>(_context.Projects).FirstOrDefaultAsync(p => p.Id == request.ProjectId);
        }
    }
}
