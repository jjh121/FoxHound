using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Blogs.GetPost;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.GetPost
{
    public class GetPostQueryHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public GetPostQueryHandlerTests()
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
            var post = _fixture.Create<Post>();
            _fixture.AddManyTo(post.Comments);

            blog.Posts.Add(post);

            _foxHoundData.Blogs.Add(blog);
            await _foxHoundData.SaveChangesAsync();

            var handler = _fixture.Create<GetPostQueryHandler>();

            // Act
            var result = await handler.Handle(new GetPostQuery(post.PostId), It.IsAny<CancellationToken>());

            // Assert
            Assert.Equal(post.PostId, result.PostId);
            Assert.Equal(post.BlogId, result.BlogId);
            Assert.Equal(post.Title, result.Title);
            Assert.Equal(post.Content, result.Content);
            Assert.Equal(post.CreatedDate, result.CreatedDate);
            Assert.NotEmpty(result.Comments);
            Assert.Equal(post.Comments.Count, result.Comments.Count);
        }
    }
}