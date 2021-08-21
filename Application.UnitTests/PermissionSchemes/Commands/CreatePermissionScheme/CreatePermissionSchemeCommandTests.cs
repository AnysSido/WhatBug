using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Result;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenDuplicateSchemeName_ReturnsNameIsTakenError()
        {
            // Arrange
            using (var context = _factory.Create())
            {
                context.PermissionSchemes.Add(new PermissionScheme { Id = 1, Name = "Software Development" });
                context.SaveChanges();
            }

            var sut = new CreatePermissionSchemeCommandHandler(_context);
            var command = new CreatePermissionSchemeCommand { Name = "Software Development" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var expectedErrorCode = Errors.PermissionScheme.NameIsTaken(command.Name).Code;
            result.Errors.Select(e => e.Code).ShouldContain(expectedErrorCode);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_CreatesPermissionScheme()
        {
            // Arrange
            var sut = new CreatePermissionSchemeCommandHandler(_context);
            var command = new CreatePermissionSchemeCommand { Name = "Scheme Name", Description = "Scheme Desc" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            var scheme = await _context.PermissionSchemes.SingleAsync(s => s.Name == command.Name);
            scheme.Name.ShouldBe(command.Name);
            scheme.Description.ShouldBe(command.Description);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReturnsSuccess()
        {
            // Arrange
            var sut = new CreatePermissionSchemeCommandHandler(_context);
            var command = new CreatePermissionSchemeCommand { Name = "Scheme Name" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
        }
    }
}
