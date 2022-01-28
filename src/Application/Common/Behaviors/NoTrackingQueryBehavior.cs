using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;

namespace WhatBug.Application.Common.Behaviors
{
    /*
     *  EF Core change tracking is not needed for queries as they never persist data back to the db.
     *  This behavior will disable change tracking for all CQRS queries (does nothing for commands).
     *
     *  Identity Resolution is still enabled to ensure that related entities are only materialized once.
     *  e.g. if 10 projects use the same permission scheme that scheme will only be returned once.
     *
     *  See: https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.querytrackingbehavior?view=efcore-5.0
     *
     *  This behavior should run after validation behaviors so that they can still use tracking.
     */

    public class NoTrackingQueryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
        where TResponse : Response
    {
        private readonly IWhatBugDbContext _context;

        public NoTrackingQueryBehavior(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var currentTracking = _context.ChangeTracker.QueryTrackingBehavior;

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            var response = await next();
            _context.ChangeTracker.QueryTrackingBehavior = currentTracking;

            return response;
        }
    }
}