using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.CreatePost;
using Xunit;

namespace FoxHound.App.Tests.Blogs.CreatePost
{
    public class CreatePostCommandValidatorTests
    {
        private readonly IFixture _fixture;

        public CreatePostCommandValidatorTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void BlogId_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreatePostCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.BlogId, 0).WithErrorMessage("Blog Id is required");
        }

        [Fact]
        public void Title_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreatePostCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Title, string.Empty).WithErrorMessage("Title is required");
        }

        [Fact]
        public void Title_IsGreaterThan128Characters_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreatePostCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Title, new string('*', 129)).WithErrorMessage("Title must be less than or equal to 128 characters");
        }

        [Fact]
        public void Content_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreatePostCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Content, string.Empty).WithErrorMessage("Content is required");
        }
    }
}