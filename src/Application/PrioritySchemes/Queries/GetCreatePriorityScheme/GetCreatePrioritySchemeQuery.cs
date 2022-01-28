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

namespace WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme
{
    [Authorize(Permissions.ManagePrioritySchemes)]
    public class GetCreatePrioritySchemeQuery : IQuery<Response<CreatePrioritySchemeDTO>>
    {
    }

    public class GetCreatePrioritySchemeQueryHandler : IRequestHandler<GetCreatePrioritySchemeQuery, Response<CreatePrioritySchemeDTO>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreatePrioritySchemeQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<CreatePrioritySchemeDTO>> Handle(GetCreatePrioritySchemeQuery request, CancellationToken cancellationToken)
        {
            var dto = new CreatePrioritySchemeDTO
            {
                Priorities = await _context.Priorities
                    .OrderBy(p => p.Order)
                    .ProjectTo<PriorityDTO>(_mapper.ConfigurationProvider).ToListAsync()
            };

            return Response<CreatePrioritySchemeDTO>.Success(dto);
        }
    }
}