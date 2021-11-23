using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    [Authorize(Permissions.ViewProject)]
    public record GetCommentsQuery : IQuery<Response<GetCommentsQueryResult>>, IRequireIssueAuthorization
    {
        public string IssueId { get; init; }
    }

    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, Response<GetCommentsQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetCommentsQueryHandler(IWhatBugDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Response<GetCommentsQueryResult>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _context.IssueComments
                .Where(c => c.IssueId == request.IssueId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            comments.ForEach(comment => comment.IsByCurrentUser = comment.Author.Id == _currentUserService.Id);

            var dto = new GetCommentsQueryResult { Comments = comments };

            return Response<GetCommentsQueryResult>.Success(dto);
        }
    }
}
