using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.PrioritySchemes.Commands.EditPriorityScheme
{
    public class EditPrioritySchemeCommandTests : CommandTestBase
    {
        public EditPrioritySchemeCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Priorities.AddRange(new[]
                {
                    new Priority { Id = 1, Name = "Prio1" },
                    new Priority { Id = 2, Name = "Prio2" },
                    new Priority { Id = 3, Name = "Prio3", IsDefault = true },
                });

                context.PrioritySchemes.AddRange(new[]
                {
                    new PriorityScheme { Id = 1, Name = "PriorityScheme1", Description = "PriorityScheme1" }
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_UpdatesPriorityScheme()
        {
            // Arrange
            var sut = new EditPrioritySchemeCommandHandler(_context);
            var command = new EditPrioritySchemeCommand
            {
                Id = 1,
                Name = "TestName",
                Description = "TestDesc",
                PriorityIds = new[] { 1, 3 }
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var scheme = _context.PrioritySchemes.First(s => s.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            scheme.Name.ShouldBe("TestName");
            scheme.Description.ShouldBe("TestDesc");
            scheme.Priorities.Count.ShouldBe(2);
            scheme.Priorities.Select(p => p.PriorityId).ShouldContain(1);
            scheme.Priorities.Select(p => p.PriorityId).ShouldContain(3);
        }

        [Fact]
        public async Task Handle_GivenNoPriorities_AssignsDefaultPriority()
        {
            // Arrange
            var sut = new EditPrioritySchemeCommandHandler(_context);
            var command = new EditPrioritySchemeCommand
            {
                Id = 1,
                Name = "TestName",
                Description = "TestDesc",
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var scheme = _context.PrioritySchemes.First(s => s.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            scheme.Priorities.Count.ShouldBe(1);
            scheme.Priorities.First().PriorityId.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_GivenNewPriorities_ReplacesOldPriorities()
        {
            // Arrange
            using (var context = CreateContext())
            {
                context.PrioritySchemePriorities.AddRange(new[]
                {
                    new PrioritySchemePriority { PrioritySchemeId = 1, PriorityId = 1 },
                    new PrioritySchemePriority { PrioritySchemeId = 1, PriorityId = 3 },
                });
            }

            var sut = new EditPrioritySchemeCommandHandler(_context);
            var command = new EditPrioritySchemeCommand
            {
                Id = 1,
                Name = "TestName",
                Description = "TestDesc",
                PriorityIds = new[] { 2 }
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var scheme = _context.PrioritySchemes.First(s => s.Id == 1);

            // Assert
            result.Succeeded.ShouldBe(true);
            scheme.Priorities.Count.ShouldBe(1);
            scheme.Priorities.First().PriorityId.ShouldBe(2);
        }
    }
}