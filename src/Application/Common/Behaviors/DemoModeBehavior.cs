using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;

namespace WhatBug.Application.Common.Behaviors
{
    public class DemoModeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TResponse : Response
    {
        private readonly ICurrentUserService _currentUserService;

        public DemoModeBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_currentUserService.IsAuthenticated && _currentUserService.IsReadOnly)
            {
                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                    return Response<int>.Success(1) as TResponse;

                return Response.Success() as TResponse;
            }

            return await next();
        }
    }
}