using FluentValidation.TestHelper;
using WhatBug.Application.Issues.Commands.CreateIssue;
using Xunit;

namespace WhatBug.Application.UnitTests.Issues.Commands
{
    public class CreateIssueValidatorTests
    {
        private readonly CreateIssueCommandValidator _validator;

        public CreateIssueValidatorTests()
        {
            _validator = new CreateIssueCommandValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenInvalidSummary_ThrowsValidationException(string summary)
        {
            // Arrange
            var command = new CreateIssueCommand { Summary = summary };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.Summary);
        }

        [Fact]
        public void GivenInvalidIds_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateIssueCommand { };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(command => command.ProjectId);
            result.ShouldHaveValidationErrorFor(command => command.PriorityId);
            result.ShouldHaveValidationErrorFor(command => command.IssueTypeId);
            result.ShouldHaveValidationErrorFor(command => command.ReporterId);
        }
    }
}
