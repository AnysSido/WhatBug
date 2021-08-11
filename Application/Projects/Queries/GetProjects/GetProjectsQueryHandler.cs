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

namespace WhatBug.Application.Projects.Queries.GetProjects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ProjectsDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectsDTO> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _mapper.ProjectTo<ProjectDTO>(_context.Projects).ToListAsync();
            var dto = new ProjectsDTO
            {
                Projects = projects
            };
            return dto;
        }
    }
}
