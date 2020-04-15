using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.Common;
using FoxHound.App.Blogs.UpdateBlog;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.UpdateBlog
{
    public class UpdateBlogCommandValidatorTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public UpdateBlogCommandValidatorTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
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

        [Fact]
        public async Task UpdateBlog_OwnerIsPartOfThisBlog_PassesFailsValidation()
        {
            // Arrange
            var owner = _fixture.Create<string>().Substring(0, 20);

            var thisBlog = _fixture.Create<Blog>();
            thisBlog.SetOwner(owner);
            _foxHoundData.Blogs.Add(thisBlog);

            await _foxHoundData.SaveChangesAsync();

            var updateBlogCommand = new UpdateBlogCommand
            {
                BlogId = thisBlog.BlogId,
                Owner = owner
            };

            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldNotHaveValidationErrorFor(x => x.Owner, updateBlogCommand);
        }

        [Fact]
        public async Task UpdateBlog_OwnerIsPartOfDifferentBlog_FailsValidation()
        {
            // Arrange
            var owner = _fixture.Create<string>().Substring(0, 20);

            var thisBlog = _fixture.Create<Blog>();
            _foxHoundData.Blogs.Add(thisBlog);

            var differentBlog = _fixture.Create<Blog>();
            differentBlog.SetOwner(owner);
            _foxHoundData.Blogs.Add(differentBlog);

            await _foxHoundData.SaveChangesAsync();

            var updateBlogCommand = new UpdateBlogCommand
            {
                BlogId = thisBlog.BlogId,
                Owner = owner
            };

            var validator = _fixture.Create<UpdateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, updateBlogCommand).WithErrorMessage("Owner already in use for a Blog");
        }
    }
}