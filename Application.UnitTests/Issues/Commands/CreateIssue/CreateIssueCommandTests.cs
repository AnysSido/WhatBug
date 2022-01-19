using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using WhatBug.Application.Issues.Commands.CreateIssue;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandTests : CommandTestBase
    {
        private CreateIssueCommand _command;

        public CreateIssueCommandTests() : base()
        {
            using (var context = CreateContext())
            {
                context.Projects.Add(new Project { Id = 1 });
                context.Priorities.Add(new Priority { Id = 1 });
                context.IssueTypes.Add(new IssueType { Id = 1 });
                context.Users.Add(new User { Id = 1 });
                context.IssueStatuses.Add(new IssueStatus { Id = 1, Name = "Backlog" });
                context.SaveChanges();
            }

            _command = new CreateIssueCommand
            {
                Summary = "Test",
                ProjectId = 1,
                PriorityId = 1,
                IssueTypeId = 1,
                ReporterId = 1,
            };
        }

        [Fact]
        public async void Handle_GivenValidRequest_CreatesIssue()
        {
            // Arrange
            var sut = new CreateIssueCommandHandler(_context);

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            result.Succeeded.ShouldBe(true);
            _context.Issues.Count().ShouldBe(1);
        }

        [Fact]
        public async void Handle_GivenValidRequest_AssignsCorrectIssueKey()
        {
            // Arrange
            var projectKey = "ABCD";
            var issueCounter = 10;

            using (var context = CreateContext())
            {
                context.Projects.Add(new Project { Id = 2, Key = projectKey, IssueCounter = issueCounter });
                context.SaveChanges();
            }

            _command.ProjectId = 2;
            var sut = new CreateIssueCommandHandler(_context);

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            using (var context = CreateContext())
            {
                var expectedKey = "ABCD-11";
                var newIssue = context.Issues.Find(expectedKey);
                newIssue.ShouldNotBeNull();
            }
        }

        [Fact]
        public async void Handle_GivenValidRequest_IncrementsProjectIssueCounter()
        {
            // Arrange
            var projectKey = "ABCD";
            var issueCounter = 10;

            using (var context = CreateContext())
            {
                context.Projects.Add(new Project { Id = 2, Key = projectKey, IssueCounter = issueCounter });
                context.SaveChanges();
            }

            _command.ProjectId = 2;
            var sut = new CreateIssueCommandHandler(_context);

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            using (var context = CreateContext())
            {
                var project = context.Projects.Single(p => p.Id == 2);
                project.IssueCounter.ShouldBe(11);
            }
        }

        [Fact]
        public async void Handle_GivenValidRequest_AssignsBacklogDefaultIssueStatus()
        {
            // Arrange
            var sut = new CreateIssueCommandHandler(_context);

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            using (var context = CreateContext())
            {
                var expectedStatusId = 1;
                var newIssue = context.Issues.Include(i => i.IssueStatus).First();
                newIssue.IssueStatus.Id.ShouldBe(expectedStatusId);
            }
        }
    }
}
