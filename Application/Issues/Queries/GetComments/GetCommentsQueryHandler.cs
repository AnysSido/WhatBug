using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, CommentsDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentsDTO> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var comments = await _context.IssueComments.Where(c => c.IssueId == request.IssueId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider).ToListAsync();

            var dto = new CommentsDTO { Comments = comments };

            return dto;
        }
    }
}
