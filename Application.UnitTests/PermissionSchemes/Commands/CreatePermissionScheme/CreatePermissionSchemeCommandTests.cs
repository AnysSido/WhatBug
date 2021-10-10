using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_CreatesPermissionScheme()
        {
            // Arrange
            var sut = new CreatePermissionSchemeCommandHandler(_context);
            var command = new CreatePermissionSchemeCommand { Name = "Scheme Name", Description = "Scheme Desc" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var scheme = await _context.PermissionSchemes.FirstOrDefaultAsync(s => s.Name == command.Name);
            scheme.ShouldNotBeNull();
            scheme.Name.ShouldBe(command.Name);
            scheme.Description.ShouldBe(command.Description);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsSuccessWithId()
        {
            // Arrange
            var sut = new CreatePermissionSchemeCommandHandler(_context);
            var command = new CreatePermissionSchemeCommand { Name = "Scheme Name" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            result.Result.ShouldBe(1);
        }
    }
}