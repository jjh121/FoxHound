﻿using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.UpdateBlog;
using Xunit;

namespace FoxHound.App.Tests.Blogs.UpdateBlog
{
    public class UpdateBlogCommandValidatorTests
    {
        private readonly IFixture _fixture;

        public UpdateBlogCommandValidatorTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void BlogId_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.BlogId, 0).WithErrorMessage("Blog Id is required");
        }

        [Fact]
        public void Title_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Title, string.Empty).WithErrorMessage("Title is required");
        }

        [Fact]
        public void Title_IsGreaterThan128Characters_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Title, new string('*', 129)).WithErrorMessage("Title must be less than or equal to 128 characters");
        }

        [Fact]
        public void Owner_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, string.Empty).WithErrorMessage("Owner is required");
        }

        [Fact]
        public void Owner_IsGreaterThan128Characters_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, new string('*', 21)).WithErrorMessage("Owner must be less than or equal to 20 characters");
        }
    }
}