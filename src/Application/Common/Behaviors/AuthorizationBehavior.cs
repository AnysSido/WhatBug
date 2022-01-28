using MediatR;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Authorization;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Security;

namespace WhatBug.Application.Common.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthorizationManager _authzManager;

        public AuthorizationBehavior(ICurrentUserService currentUserService, IAuthorizationManager authzManager)
        {
            _currentUserService = currentUserService;
            _authzManager = authzManager;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                if (!_currentUserService.IsAuthenticated)
                    throw new AccessDeniedException();

                var projectId = request is IRequireProjectAuthorization projReq ? projReq.ProjectId : default;
                var issueId =  request is IRequireIssueAuthorization issueReq ? issueReq.IssueId : default;

                var authorized = false;

                foreach (var authorizeAttribute in authorizeAttributes)
                {
                    if (authorizeAttribute.Operator == PermissionOperator.And)
                        authorized = await _authzManager.HasAllPermissions(authorizeAttribute.Permissions, projectId, issueId);
                    else
                        authorized = await _authzManager.HasAnyPermission(authorizeAttribute.Permissions, projectId, issueId);

                    if (authorized)
                        break;
                }

                if (!authorized)
                    throw new AccessDeniedException();
            }

            return await next();
        }
    }
}