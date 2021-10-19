using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public record GetEditPriorityQuery : IQuery<Response<EditPriorityDTO>>
    {
        public int Id { get; set; }
    }

    public class GetEditPriorityQueryHandler : IRequestHandler<GetEditPriorityQuery, Response<EditPriorityDTO>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPriorityQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<EditPriorityDTO>> Handle(GetEditPriorityQuery request, CancellationToken cancellationToken)
        {
            var dto = await _mapper.ProjectTo<EditPriorityDTO>(_context.Priorities).FirstOrDefaultAsync(p => p.Id == request.Id);

            if (dto == null)
            {
                // TODO: Throw not found exception
            }

            dto.Colors = await _context.Colors.OrderBy(c => c.Id).ProjectTo<ColorDTO>(_mapper.ConfigurationProvider).ToListAsync();
            dto.Icons = await _mapper.ProjectTo<IconDTO>(_context.Icons).ToListAsync();

            return Response<EditPriorityDTO>.Success(dto);
        }
    }
}