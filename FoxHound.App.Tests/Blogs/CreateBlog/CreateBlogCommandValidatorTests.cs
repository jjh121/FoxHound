using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.CreateBlog;
using Xunit;

namespace FoxHound.App.Tests.Blogs.CreateBlog
{
    public class CreateBlogCommandValidatorTests
    {
        private readonly IFixture _fixture;

        public CreateBlogCommandValidatorTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void Owner_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, string.Empty).WithErrorMessage("Owner is required");
        }

        [Fact]
        public void Owner_IsGreaterThan20Characters_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, new string('*', 21)).WithErrorMessage("Owner must be less than or equal to 20 characters");
        }
    }
}