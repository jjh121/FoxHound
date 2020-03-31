using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Blogs.GetBlog;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.GetBlog
{
    public class GetBlogQueryHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public GetBlogQueryHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
        }

        [Fact]
        public async Task Handle_GetBlogById_MapsResultCorrectly()
        {
            // Arrange
            var blog = _fixture.Create<Blog>();
            _fixture.AddManyTo(blog.Posts);

            _foxHoundData.Blogs.Add(blog);
            await _foxHoundData.SaveChangesAsync();

            var handler = _fixture.Create<GetBlogQueryHandler>();

            // Act
            var result = await handler.Handle(new GetBlogQuery(blog.BlogId), It.IsAny<CancellationToken>());

            // Assert
            Assert.Equal(blog.BlogId, result.BlogId);
            Assert.Equal(blog.Title, result.Title);
            Assert.Equal(blog.Owner, result.Owner);
            Assert.Equal(blog.CreatedDate, result.CreatedDate);
            Assert.NotEmpty(result.Posts);
            Assert.Equal(blog.Posts.Count, result.Posts.Count);
        }
    }
}