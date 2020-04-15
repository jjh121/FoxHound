using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.Common;
using Moq;
using Xunit;

namespace FoxHound.App.Tests.Blogs.Common
{
    public class CommonBlogCommandValidatorTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IBlogCommand> _blogMock;

        public CommonBlogCommandValidatorTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _blogMock = new Mock<IBlogCommand>();
        }

        [Fact]
        public void Title_IsEmpty_FailsValidation()
        {
            // Arrange
            _blogMock.Setup(x => x.Title).Returns(string.Empty);

            var validator = _fixture.Create<CommonBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Title, _blogMock.Object).WithErrorMessage("Title is required");
        }

        [Fact]
        public void Title_IsGreaterThan128Characters_FailsValidation()
        {
            // Arrange
            _blogMock.Setup(x => x.Title).Returns(new string('*', 129));

            var validator = _fixture.Create<CommonBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Title, _blogMock.Object).WithErrorMessage("Title must be less than or equal to 128 characters");
        }

        [Fact]
        public void Owner_IsEmpty_FailsValidation()
        {
            // Arrange
            _blogMock.Setup(x => x.Owner).Returns(string.Empty);

            var validator = _fixture.Create<CommonBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, _blogMock.Object).WithErrorMessage("Owner is required");
        }

        [Fact]
        public void Owner_IsGreaterThan128Characters_FailsValidation()
        {
            // Arrange
            _blogMock.Setup(x => x.Owner).Returns(new string('*', 21));

            var validator = _fixture.Create<CommonBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, _blogMock.Object).WithErrorMessage("Owner must be less than or equal to 20 characters");
        }
    }
}
