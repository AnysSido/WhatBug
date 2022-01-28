using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Commands.DeleteRole;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Commands.DeleteRole
{
    public class DeleteRoleCommandTests : CommandTestBase
    {
        public DeleteRoleCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Roles.AddRange(new[]
                {
                    new Role { Id = 1, Name = "Developer", Description = "Developer Role" },
                    new Role { Id = 2, Name = "QA", Description = "QA Role" }
                });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_DeletesRole()
        {
            // Arrange
            var sut = new DeleteRoleCommandHandler(_context);
            var command = new DeleteRoleCommand { RoleId = 1 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var roles = await _context.Roles.ToListAsync();

            // Assert
            result.Succeeded.ShouldBe(true);
            roles.Count.ShouldBe(1);
            roles.First().Id.ShouldBe(2);
        }
    }
}