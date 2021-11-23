using Shouldly;
using System.Linq;
using System.Reflection;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Analysis
{
    public class AnalysisTests
    {
        [Fact]
        public void IQuery_RequiresAuthorizeAttribute()
        {
            var assembly = Assembly.GetAssembly(typeof(IQuery<>));

            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>)))
                .ToList();

            var typesMissingAttribute = types.Any(t => t.GetCustomAttribute<AuthorizeAttribute>() == null);

            typesMissingAttribute.ShouldBeFalse();
        }

        [Fact]
        public void ICommand_RequiresAuthorizeAttributeOrNoAuthorizeAttribute()
        {
            var assembly = Assembly.GetAssembly(typeof(ICommand<>));

            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>)))
                .ToList();

            var typesMissingAttribute = types.Any(t => 
                t.GetCustomAttribute<AuthorizeAttribute>() == null 
                && t.GetCustomAttribute<NoAuthorizeAttribute>() == null);

            typesMissingAttribute.ShouldBeFalse();
        }

        [Fact]
        public void AuthorizeAttribute_WhenGivenProjectPermission_RequiresProjectOrIssueInterface()
        {
            var assembly = Assembly.GetAssembly(typeof(IQuery<>));
            var types = assembly.GetTypes().Where(t => t.GetCustomAttribute<AuthorizeAttribute>() != null);

            foreach (var type in types)
            {
                var projectPermissions = type.GetCustomAttribute<AuthorizeAttribute>().Permissions
                    .Select(p => Permissions.ToEntity(p))
                    .Where(p => p.Type == PermissionType.Project);

                if (projectPermissions.Any())
                {
                    var hasRequiredProjectInterface = type.GetInterfaces().Contains(typeof(IRequireProjectAuthorization));
                    var hasRequiredIssueInterface = type.GetInterfaces().Contains(typeof(IRequireIssueAuthorization));

                    (hasRequiredIssueInterface || hasRequiredProjectInterface).ShouldBeTrue();
                }
            }
        }
    }
}