using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Blogs.GetAllBlogs;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.GetAllBlogs
{
    public class GetAllBlogsQueryHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public GetAllBlogsQueryHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
        }

        [Fact]
        public async Task Handle_GetAllBlogs_ReturnsCorrectCount()
        {
            // Arrange
            var blogs = _fixture.CreateMany<Blog>().ToList();

            _foxHoundData.Blogs.AddRange(blogs);
            await _foxHoundData.SaveChangesAsync();

            var handler = _fixture.Create<GetAllBlogsQueryHandler>();

            // Act
            var result = await handler.Handle(new GetAllBlogsQuery(), It.IsAny<CancellationToken>());

            // Assert
            Assert.Equal(blogs.Count, result.Count);
        }

        [Fact]
        public async Task Handle_GetAllBlogs_MapsResultCorrectly()
        {
            // Arrange
            var blog = _fixture.Create<Blog>();
            _fixture.AddManyTo(blog.Posts);

            _foxHoundData.Blogs.Add(blog);
            await _foxHoundData.SaveChangesAsync();

            var handler = _fixture.Create<GetAllBlogsQueryHandler>();

            // Act
            var result = await handler.Handle(new GetAllBlogsQuery(), It.IsAny<CancellationToken>());

            // Assert
            Assert.Single(result);

            var blogResult = result.Single();

            Assert.Equal(blog.BlogId, blogResult.BlogId);
            Assert.Equal(blog.Title, blogResult.Title);
            Assert.Equal(blog.Owner, blogResult.Owner);
            Assert.Equal(blog.CreatedDate, blogResult.CreatedDate);
            Assert.NotEmpty(blogResult.Posts);
            Assert.Equal(blog.Posts.Count, blogResult.Posts.Count);
        }
    }
}