using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Admin.Commands.CreateRole;
using WhatBug.Application.Common.Result;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Admin.Commands.CreateRole
{
    public class CreateRoleCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenDuplicateRoleName_ReturnsNameIsTakenError()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.Roles.Add(new Role { Id = 1, Name = "Admin" });
                context.SaveChanges();
            }

            var sut = new CreateRoleCommandHandler(_context);
            var command = new CreateRoleCommand { Name = "Admin" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var expectedErrorCode = Errors.Admin.Roles.NameIsTaken(command.Name).Code;
            result.Errors.Select(e => e.Code).ShouldContain(expectedErrorCode);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_CreatesRole()
        {
            // Arrange
            var sut = new CreateRoleCommandHandler(_context);
            var command = new CreateRoleCommand { Name = "New Role", Description = "Role Description" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var role = _context.Roles.Single(r => r.Name == command.Name);
            role.Name.ShouldBe(command.Name);
            role.Description.ShouldBe(command.Description);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsSuccess()
        {
            // Arrange
            var sut = new CreateRoleCommandHandler(_context);
            var command = new CreateRoleCommand { Name = "Role Name" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
        }
    }
}
