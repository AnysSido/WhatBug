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

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailQueryHandler : IRequestHandler<GetIssueDetailQuery, IssueDetailDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetIssueDetailQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IssueDetailDTO> Handle(GetIssueDetailQuery request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var dto = await _mapper.ProjectTo<IssueDetailDTO>(_context.Issues).FirstOrDefaultAsync(i => i.Id == request.Id);

            if (dto == null)
            {
                // TODO: Throw not found exception
            }

            return dto;
        }
    }
}
