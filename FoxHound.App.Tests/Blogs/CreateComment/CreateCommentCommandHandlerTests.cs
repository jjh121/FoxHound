using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Blogs.CreateComment;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.CreateComment
{
    public class CreateCommentCommandHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public CreateCommentCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
        }

        [Fact]
        public async Task Handle_WithCommand_AddsCommentToDatabase()
        {
            // Arrange
            var post = _fixture.Create<Post>();
            _fixture.AddManyTo(post.Comments);

            _foxHoundData.Posts.Add(post);
            await _foxHoundData.SaveChangesAsync();

            var command = _fixture.Build<CreateCommentCommand>()
                .With(x => x.PostId, post.PostId)
                .Create();

            var handler = _fixture.Create<CreateCommentCommandHandler>();

            // Act
            var result = await handler.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            using (var foxHoundData = new FoxHoundData(_dbContextOptions))
            {
                var postResult = await foxHoundData.Posts.Include(x => x.Comments).SingleAsync(x => x.PostId == post.PostId);
                var commentResult = postResult.Comments.Single(x => x.CommentId == result.CommentId);

                Assert.Equal(command.Author, commentResult.Author);
                Assert.Equal(command.Content, commentResult.Content);
            }
        }

        [Fact]
        public async Task Handle_WithCommand_ReturnsCommentResult()
        {
            // Arrange
            var post = _fixture.Create<Post>();
            _fixture.AddManyTo(post.Comments);

            _foxHoundData.Posts.Add(post);
            await _foxHoundData.SaveChangesAsync();

            var command = _fixture.Build<CreateCommentCommand>()
                .With(x => x.PostId, post.PostId)
                .Create();

            var handler = _fixture.Create<CreateCommentCommandHandler>();

            // Act
            var result = await handler.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            using (var foxHoundData = new FoxHoundData(_dbContextOptions))
            {
                var postResult = await foxHoundData.Posts.Include(x => x.Comments).SingleAsync(x => x.PostId == post.PostId);
                var expectedResult = postResult.Comments.Single(x => x.CommentId == result.CommentId);

                Assert.Equal(expectedResult.CommentId, result.CommentId);
                Assert.Equal(expectedResult.Author, result.Author);
                Assert.Equal(expectedResult.Content, result.Content);
                Assert.Equal(expectedResult.CreatedDate, result.CreatedDate);
            }
        }
    }
}