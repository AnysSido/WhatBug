using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserInfoDTO> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Users.Where(u => u.Id == request.UserId)
                .ProjectTo<UserInfoDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return dto;
        }
    }
}
