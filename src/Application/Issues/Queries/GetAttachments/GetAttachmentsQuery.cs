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

namespace WhatBug.Application.Issues.Queries.GetAttachments
{
    [Authorize(Permissions.ViewProject)]
    public record GetAttachmentsQuery : IQuery<Response<GetAttachmentsQueryResult>>, IRequireIssueAuthorization
    {
        public string IssueId { get; set; }
    }

    public class GetAttachmentsQueryHandler : IRequestHandler<GetAttachmentsQuery, Response<GetAttachmentsQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetAttachmentsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetAttachmentsQueryResult>> Handle(GetAttachmentsQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _context.Attachments
                .Where(a => a.IssueId == request.IssueId)
                .ProjectTo<AttachmentDTO>(_mapper.ConfigurationProvider).ToListAsync();

            var dto = new GetAttachmentsQueryResult { Attachments = attachments };

            return Response<GetAttachmentsQueryResult>.Success(dto);
        }
    }
}
