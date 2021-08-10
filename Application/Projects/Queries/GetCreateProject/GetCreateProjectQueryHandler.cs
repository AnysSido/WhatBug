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

namespace WhatBug.Application.Projects.Queries.GetCreateProject
{
    public class GetCreateProjectQueryHandler : IRequestHandler<GetCreateProjectQuery, CreateProjectDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreateProjectQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateProjectDTO> Handle(GetCreateProjectQuery request, CancellationToken cancellationToken)
        {
            var dto = new CreateProjectDTO
            {
                PrioritySchemes = await _mapper.ProjectTo<PrioritySchemeDTO>(_context.PrioritySchemes).ToListAsync()
            };
            return dto;
        }
    }
}
