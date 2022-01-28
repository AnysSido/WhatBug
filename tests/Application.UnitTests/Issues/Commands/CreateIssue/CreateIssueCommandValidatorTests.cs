using FluentValidation.TestHelper;
using System;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Issues.Commands.CreateIssue;
using WhatBug.Application.UnitTests.Common;
using Xunit;

namespace WhatBug.Application.UnitTests.Issues.Commands.CreateIssue
{
    [Collection("ValidatorCollection")]
    public class CreateIssueCommandValidatorTests
    {
        private readonly CreateIssueCommandValidator _sut;

        public CreateIssueCommandValidatorTests(ValidatorTestFixture fixture)
        {
            _sut = new CreateIssueCommandValidator(fixture.CreateContext());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenNullOrEmptySummary_HasValidationError(string summary)
        {
            // Arrange
            var command = new CreateIssueCommand { Summary = summary };

            // Act
            var result = _sut.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Summary)
                .WithErrorMessage("Issue summary cannot be empty");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerProjectId_HasArgumentException(int id)
        {
            // Arrange
            var command = new CreateIssueCommand { ProjectId = id };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.ProjectId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidProjectId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new CreateIssueCommand { ProjectId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.ProjectId, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerPriorityId_HasArgumentException(int id)
        {
            // Arrange
            var command = new CreateIssueCommand { PriorityId = id };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PriorityId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidPriorityId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new CreateIssueCommand { PriorityId = 5 };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.PriorityId, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerIssueTypeId_HasArgumentException(int id)
        {
            // Arrange
            var command = new CreateIssueCommand { IssueTypeId = id };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.IssueTypeId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidIssueTypeId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new CreateIssueCommand { IssueTypeId = 5 };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.IssueTypeId, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerAssigneeId_HasArgumentException(int id)
        {
            // Arrange
            var command = new CreateIssueCommand { AssigneeId = id };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.AssigneeId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidAssigneeId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new CreateIssueCommand { AssigneeId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.AssigneeId, typeof(RecordNotFoundException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void Given_ZeroOrLowerReporterId_HasArgumentException(int id)
        {
            // Arrange
            var command = new CreateIssueCommand { ReporterId = id };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.ReporterId, typeof(ArgumentException));
        }

        [Fact]
        public async void Given_InvalidReporterId_HasRecordNotFoundException()
        {
            // Arrange
            var command = new CreateIssueCommand { ReporterId = 4 };

            // Act
            var result = await _sut.TestValidateAsync(command);

            // Assert
            result.ShouldHaveExceptionFor(command => command.ReporterId, typeof(RecordNotFoundException));
        }
    }
}
