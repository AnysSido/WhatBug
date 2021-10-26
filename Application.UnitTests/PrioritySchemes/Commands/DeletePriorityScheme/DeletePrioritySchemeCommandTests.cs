using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Commands.DeletePriorityScheme;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Commands.DeletePriorityScheme
{
    public class DeletePrioritySchemeCommandTests : CommandTestBase
    {
        public DeletePrioritySchemeCommandTests()
        {
            using (var context = CreateContext())
            {
                context.PrioritySchemes.AddRange(new[]
                {
                    new PriorityScheme {Id = 1, IsDefault = true},
                    new PriorityScheme {Id = 2}
                });

                context.Projects.Add(new Project { Id = 1, PrioritySchemeId = 2 });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_DeletesScheme()
        {
            // Arrange
            var sut = new DeletePrioritySchemeCommandHandler(_context);
            var command = new DeletePrioritySchemeCommand { SchemeId = 2 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var schemes = _context.PrioritySchemes.ToList();

            // Assert
            result.Succeeded.ShouldBe(true);
            schemes.Count.ShouldBe(1);
            schemes.First().Id.ShouldBe(1);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReassignsProjectsToDefaultPriorityScheme()
        {
            // Arrange
            var sut = new DeletePrioritySchemeCommandHandler(_context);
            var command = new DeletePrioritySchemeCommand { SchemeId = 2 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var project = _context.Projects.First(p => p.Id == 1);

            // Assert
            project.PrioritySchemeId.ShouldBe(1);
        }
    }
}