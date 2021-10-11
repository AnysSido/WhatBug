using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.DeletePermissionScheme;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.PermissionSchemes.Commands.DeletePermissionScheme
{
    public class DeletePermissionSchemeCommandTests : CommandTestBase
    {
        public DeletePermissionSchemeCommandTests()
        {
            using (var context = CreateContext())
            {
                context.PermissionSchemes.AddRange(new[]
                {
                    new PermissionScheme { Id = 1, Name = "Scheme1", Description = "SchemeDesc1" },
                    new PermissionScheme { Id = 2, Name = "Scheme2", Description = "SchemeDesc2" }
                });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_DeletesScheme()
        {
            // Arrange
            var sut = new DeletePermissionSchemeCommandHandler(_context);
            var command = new DeletePermissionSchemeCommand { SchemeId = 1 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var schemes = await _context.PermissionSchemes.ToListAsync();

            // Assert
            result.Succeeded.ShouldBe(true);
            schemes.Count.ShouldBe(1);
            schemes.First().Id.ShouldBe(2);
        }
    }
}