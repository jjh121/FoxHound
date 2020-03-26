using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Blogs.CreateBlog;
using FoxHound.App.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.CreateBlog
{
    public class CreateBlogCommandHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public CreateBlogCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
        }

        [Fact]
        public async Task Handle_WithCommand_AddsBlogToDatabase()
        {
            // Arrange
            var command = _fixture.Create<CreateBlogCommand>();

            var handler = _fixture.Create<CreateBlogCommandHandler>();

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            using (var foxHoundData = new FoxHoundData(_dbContextOptions))
            {
                var result = await foxHoundData.Blogs.SingleAsync();

                Assert.Equal(command.Owner, result.Owner);
                Assert.True(result.BlogId > 0);
            }
        }

        [Fact]
        public async Task Handle_WithCommand_ReturnsBlogId()
        {
            // Arrange
            var command = _fixture.Create<CreateBlogCommand>();

            var handler = _fixture.Create<CreateBlogCommandHandler>();

            // Act
            var result = await handler.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            Assert.True(result > 0);
        }
    }
}