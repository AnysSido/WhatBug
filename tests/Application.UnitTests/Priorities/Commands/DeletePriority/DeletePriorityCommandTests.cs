using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Commands.DeletePriority;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Priorities.Commands.DeletePriority
{
    public class DeletePriorityCommandTests : CommandTestBase
    {
        public DeletePriorityCommandTests()
        {
            using (var context = CreateContext())
            {
                context.Priorities.AddRange(new[]
                {
                    new Priority { Id = 1, IsDefault = true },
                    new Priority { Id = 2 },
                });

                context.Issues.Add(new Issue { Id = "DEMO-1", PriorityId = 2 });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Handle_GivenValidRequest_DeletesPriority()
        {
            // Arrange
            var sut = new DeletePriorityCommandHandler(_context);
            var command = new DeletePriorityCommand { PriorityId = 2 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var priorities = await _context.Priorities.ToListAsync();

            // Assert
            result.Succeeded.ShouldBe(true);
            priorities.Count.ShouldBe(1);
            priorities.First().Id.ShouldBe(1);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ReassignsIssuesToDefaultPriority()
        {
            // Arrange
            var sut = new DeletePriorityCommandHandler(_context);
            var command = new DeletePriorityCommand { PriorityId = 2 };

            // Act
            var result = await sut.Handle(command, CancellationToken.None);
            var project = _context.Issues.First(p => p.Id == "DEMO-1");

            // Assert
            project.PriorityId.ShouldBe(1);
        }
    }
}
