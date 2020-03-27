using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.CreateBlog;
using FoxHound.App.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace FoxHound.App.Tests.Blogs.CreateBlog
{
    public class CreateBlogCommandValidatorTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public CreateBlogCommandValidatorTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
        }

        [Fact]
        public void Owner_IsEmpty_FailsValidation()
        {
            // Arrange
            var validator = _fixture.Create<CreateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, string.Empty).WithErrorMessage("Owner is required");
        }
    }
}