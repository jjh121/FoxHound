using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.Common;
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
        public void CreateBlogCommand_CallsCommonCommandValidator()
        {
            // Arrange
            var validator = _fixture.Create<CreateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveChildValidator(x => x, typeof(CommonBlogCommandValidator));
        }
    }
}