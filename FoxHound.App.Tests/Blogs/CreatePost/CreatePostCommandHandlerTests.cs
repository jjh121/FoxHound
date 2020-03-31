using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Blogs.CreatePost;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.CreatePost
{
    public class CreatePostCommandHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public CreatePostCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
        }

        [Fact]
        public async Task Handle_WithCommand_AddsPostToBlogAndSavesToDatabase()
        {
            // Arrange
            var blog = _fixture.Create<Blog>();
            _fixture.AddManyTo(blog.Posts);

            _foxHoundData.Blogs.Add(blog);
            await _foxHoundData.SaveChangesAsync();

            var command = _fixture.Build<CreatePostCommand>()
                .With(x => x.BlogId, blog.BlogId)
                .Create();

            var handler = _fixture.Create<CreatePostCommandHandler>();

            // Act
            var newPostId = await handler.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            using (var foxHoundData = new FoxHoundData(_dbContextOptions))
            {
                var blogResult = await foxHoundData.Blogs.Include(x => x.Posts).SingleAsync(x => x.BlogId == blog.BlogId);
                var postResult = blogResult.Posts.Single(x => x.PostId == newPostId);

                Assert.Equal(command.Title, postResult.Title);
                Assert.Equal(command.Content, postResult.Content);
            }
        }
    }
}