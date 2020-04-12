using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Blogs.UpdateBlog;
using FoxHound.App.Data;
using FoxHound.App.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FoxHound.App.Tests.Blogs.UpdateBlog
{
    public class UpdateBlogCommandHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IFoxHoundData _foxHoundData;
        private readonly DbContextOptions<FoxHoundData> _dbContextOptions;

        public UpdateBlogCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _dbContextOptions = new DbContextOptionsBuilder<FoxHoundData>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _foxHoundData = new FoxHoundData(_dbContextOptions);
            _fixture.Inject(_foxHoundData);
        }

        [Fact]
        public async Task Handle_TitleAndOwner_UpdatesDatabase()
        {
            // Arrange
            var blog = _fixture.Create<Blog>();

            _foxHoundData.Blogs.Add(blog);
            await _foxHoundData.SaveChangesAsync();

            var command = _fixture.Build<UpdateBlogCommand>()
                .With(x => x.BlogId, blog.BlogId)
                .Create();

            var handler = _fixture.Create<UpdateBlogCommandHandler>();

            // Act
            await handler.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            using (var foxHoundData = new FoxHoundData(_dbContextOptions))
            {
                Blog blogResult = await foxHoundData.Blogs.FindAsync(blog.BlogId);

                Assert.Equal(command.Title, blogResult.Title);
                Assert.Equal(command.Owner, blogResult.Owner);
            }
        }
    }
}