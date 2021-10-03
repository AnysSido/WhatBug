using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Commands.EditRole;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.ProjectRoles.Commands.EditRole
{
    public class EditRoleCommandTests : CommandTestBase
    {
        public EditRoleCommandTests()
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
        public async Task Handle_GivenValidRequest_ReturnsSuccess()
        {
            // Arrange
            var sut = new EditRoleCommandHandler(_context);
            var command = new EditRoleCommand { RoleId = 1, Name = "New Name", Description = "New Description" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var role = _context.Roles.SingleOrDefault(r => r.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
        }

        [Fact]
        public async Task Handle_GivenNewName_UpdatesName()
        {
            // Arrange
            var sut = new EditRoleCommandHandler(_context);
            var command = new EditRoleCommand { RoleId = 1, Name = "Admin" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var role = _context.Roles.SingleOrDefault(r => r.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            role.Name.ShouldBe("Admin");
        }

        [Fact]
        public async Task Handle_GivenNewDescription_UpdatesDescription()
        {
            // Arrange
            var sut = new EditRoleCommandHandler(_context);
            var command = new EditRoleCommand { RoleId = 1, Description = "New Description" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var role = _context.Roles.SingleOrDefault(r => r.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            role.Description.ShouldBe("New Description");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Handle_GivenNullOrEmptyDescription_SetsNullDescription(string description)
        {
            // Arrange
            var sut = new EditRoleCommandHandler(_context);
            var command = new EditRoleCommand { RoleId = 1, Description = description };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var role = _context.Roles.SingleOrDefault(r => r.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            role.Description.ShouldBe(null);
        }
    }
}