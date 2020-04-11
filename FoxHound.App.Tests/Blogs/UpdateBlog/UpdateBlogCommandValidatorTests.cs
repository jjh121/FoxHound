using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.Common;
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
        public void UpdateBlogCommand_CallsCommonCommandValidator()
        {
            // Arrange
            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveChildValidator(x => x, typeof(CommonBlogCommandValidator));
        }
    }
}