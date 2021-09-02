using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Queries.GetAttachments
{
    public class GetAttachmentsQueryHandler : IRequestHandler<GetAttachmentsQuery, AttachmentsDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetAttachmentsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AttachmentsDTO> Handle(GetAttachmentsQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _context.Attachments.Where(a => a.IssueId == request.IssueId)
                .ProjectTo<AttachmentDTO>(_mapper.ConfigurationProvider).ToListAsync();

            var dto = new AttachmentsDTO { Attachments = attachments };

            return dto;
        }
    }
}
