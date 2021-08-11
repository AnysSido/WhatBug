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
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme
{
    public class EditPrioritySchemeCommandHandler : IRequestHandler<EditPrioritySchemeCommand>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public EditPrioritySchemeCommandHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditPrioritySchemeCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var scheme = await _context.PrioritySchemes
                .Include(s => s.Priorities).Where(s => s.Id == request.Id).FirstOrDefaultAsync();

            if (scheme == null)
            {
                // TODO: Throw not found exception
            }

            var priorities = await _context.Priorities.Where(p => request.PriorityIds.Contains(p.Id)).ToListAsync();

            scheme.Name = request.Name;
            scheme.Description = request.Description;
            scheme.Priorities = priorities;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
