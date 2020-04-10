using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.CreateComment;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FoxHound.App.Tests.Blogs.CreateComment
{
    public class CreateCommentCommandValidatorTests
    {
        private readonly IFixture _fixture;

        public CreateCommentCommandValidatorTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void PostId_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreateCommentCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.PostId, 0).WithErrorMessage("Post Id is required");
        }

        [Fact]
        public void Author_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreateCommentCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Author, string.Empty).WithErrorMessage("Author is required");
        }

        [Fact]
        public void Author_IsGreaterThan20Characters_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreateCommentCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Author, new string('*', 21)).WithErrorMessage("Author must be less than or equal to 20 characters");
        }

        [Fact]
        public void Content_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreateCommentCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Content, string.Empty).WithErrorMessage("Content is required");
        }
    }
}
