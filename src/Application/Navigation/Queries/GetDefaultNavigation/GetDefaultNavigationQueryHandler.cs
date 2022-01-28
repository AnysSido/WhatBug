using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using static WhatBug.Application.Navigation.Queries.GetDefaultNavigation.DefaultNavigationDTO;

namespace WhatBug.Application.Navigation.Queries.GetDefaultNavigation
{
    public class GetDefaultNavigationQueryHandler : IRequestHandler<GetDefaultNavigationQuery, DefaultNavigationDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetDefaultNavigationQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DefaultNavigationDTO> Handle(GetDefaultNavigationQuery request, CancellationToken cancellationToken)
        {
            var projects = await _mapper.ProjectTo<ProjectDTO>(_context.Projects).ToListAsync();

            return new DefaultNavigationDTO { Projects = projects };
        }
    }
}
