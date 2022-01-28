using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.EditPermissionScheme;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.EditPermissionScheme
{
    public class EditPermissionSchemeCommandTests : CommandTestBase
    {
        public EditPermissionSchemeCommandTests()
        {
            using (var context = CreateContext())
            {
                context.PermissionSchemes.AddRange(new[]
                {
                    new PermissionScheme { Id = 1, Name = "SchemeName1", Description = "SchemeDesc1" },
                    new PermissionScheme { Id = 2, Name = "SchemeName2", Description = "SchemeDesc2" }
                });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_UpdatesPermissionScheme()
        {
            // Arrange
            var sut = new EditPermissionSchemeCommandHandler(_context);
            var command = new EditPermissionSchemeCommand { SchemeId = 1, Name = "New Name", Description = "New Description" };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var scheme = _context.PermissionSchemes.SingleOrDefault(r => r.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            scheme.ShouldNotBeNull();
            scheme.Name.ShouldBe("New Name");
            scheme.Description.ShouldBe("New Description");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Handle_GivenNullOrEmptyDescription_SetsNullDescription(string description)
        {
            // Arrange
            var sut = new EditPermissionSchemeCommandHandler(_context);
            var command = new EditPermissionSchemeCommand { SchemeId = 1, Description = description };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var scheme = _context.PermissionSchemes.SingleOrDefault(r => r.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            scheme.Description.ShouldBe(null);
        }
    }
}