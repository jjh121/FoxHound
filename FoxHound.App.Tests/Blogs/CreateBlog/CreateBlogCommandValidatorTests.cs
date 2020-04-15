using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using FoxHound.App.Blogs.Common;
using FoxHound.App.Blogs.CreateBlog;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
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
        public void CreateBlogCommand_CallsCommonCommandValidator()
        {
            // Arrange
            var validator = _fixture.Create<CreateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveChildValidator(x => x, typeof(CommonBlogCommandValidator));
        }

        [Fact]
        public async Task CreateBlog_OwnerIsPartOfDifferentBlog_FailsValidation()
        {
            // Arrange
            var owner = _fixture.Create<string>().Substring(0, 20);

            var blogWithSameOwner = _fixture.Create<Blog>();
            blogWithSameOwner.SetOwner(owner);
            _foxHoundData.Blogs.Add(blogWithSameOwner);

            await _foxHoundData.SaveChangesAsync();

            var validator = _fixture.Create<CreateBlogCommandValidator>();

            // Act / Assert
            validator.ShouldHaveValidationErrorFor(x => x.Owner, owner).WithErrorMessage("Owner already in use for a Blog");
        }
    }
}