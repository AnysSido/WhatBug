using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System.Linq;
using System.Threading;
using WhatBug.Application.Common.Result;
using WhatBug.Application.Issues.Commands.CreateIssue;
using WhatBug.Domain.Entities;
using Xunit;

namespace WhatBug.Application.UnitTests.Issues.Commands
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

        [Fact]
        public async void Handle_GivenInvalidProjectId_ReturnsProjectNotFoundValidationError()
        {
            // Arrange
            var sut = new CreateIssueCommandHandler(_context);
            _command.ProjectId = 2;

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            var expectedErrorCode = Errors.Issues.ProjectNotFound(_command.ProjectId).Code;
            result.Succeeded.ShouldBe(false);
            result.Errors.ShouldNotBeNull();
            result.Errors.Select(e => e.Code).ShouldContain(expectedErrorCode);
        }

        [Fact]
        public async void Handle_GivenInvalidPriorityId_ReturnsPriorityNotFoundValidationError()
        {
            // Arrange
            var sut = new CreateIssueCommandHandler(_context);
            _command.PriorityId = 2;

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            var expectedErrorCode = Errors.Issues.PriorityNotFound(_command.PriorityId).Code;
            result.Succeeded.ShouldBe(false);
            result.Errors.ShouldNotBeNull();
            result.Errors.Select(e => e.Code).ShouldContain(expectedErrorCode);
        }

        [Fact]
        public async void Handle_GivenInvalidIssueTypeId_ReturnsIssueTypeNotFoundValidationError()
        {
            // Arrange
            var sut = new CreateIssueCommandHandler(_context);
            _command.IssueTypeId = 2;

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            var expectedErrorCode = Errors.Issues.IssueTypeNotFound(_command.PriorityId).Code;
            result.Succeeded.ShouldBe(false);
            result.Errors.ShouldNotBeNull();
            result.Errors.Select(e => e.Code).ShouldContain(expectedErrorCode);
        }

        [Fact]
        public async void Handle_GivenInvalidReporterId_ReturnsReporterNotFoundValidationError()
        {
            // Arrange
            var sut = new CreateIssueCommandHandler(_context);
            _command.ReporterId = 2;

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            var expectedErrorCode = Errors.Issues.ReporterNotFound(_command.ReporterId).Code;
            result.Succeeded.ShouldBe(false);
            result.Errors.ShouldNotBeNull();
            result.Errors.Select(e => e.Code).ShouldContain(expectedErrorCode);
        }

        [Fact]
        public async void Handle_GivenInvalidAssigneeId_ReturnsAssigneeNotFoundValidationError()
        {
            // Arrange
            var sut = new CreateIssueCommandHandler(_context);
            _command.AssigneeId = 2;

            // Act
            var result = await sut.Handle(_command, CancellationToken.None);

            // Assert
            var expectedErrorCode = Errors.Issues.AssigneeNotFound((int)_command.AssigneeId).Code;
            result.Succeeded.ShouldBe(false);
            result.Errors.ShouldNotBeNull();
            result.Errors.Select(e => e.Code).ShouldContain(expectedErrorCode);
        }
    }
}
