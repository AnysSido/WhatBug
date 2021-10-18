using Application.UnitTests.Common;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Commands.ReorderPriorities;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands
{
    public class ReorderPrioritiesCommandTests : CommandTestBase
    {
        public ReorderPrioritiesCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Priorities.AddRange(new[] {
                    new Priority { Id = 1, Name = "Priority1", Description = "PriorityDesc1", Order = 1 },
                    new Priority { Id = 2, Name = "Priority2", Description = "PriorityDesc2", Order = 2 },
                    new Priority { Id = 3, Name = "Priority3", Description = "PriorityDesc3", Order = 3 },
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReordersPriorities()
        {
            // Arrange
            var sut = new ReorderPrioritiesCommandHandler(_context);
            var command = new ReorderPrioritiesCommand
            {
                Ids = new[] { 2, 3, 1 }
            };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var priorities = _context.Priorities.ToList();

            // Assert
            result.Succeeded.ShouldBe(true);
            priorities.OrderBy(p => p.Order).Select(p => p.Id).ToArray().ShouldBeEquivalentTo(new[] { 2, 3, 1 });
        }
    }
}