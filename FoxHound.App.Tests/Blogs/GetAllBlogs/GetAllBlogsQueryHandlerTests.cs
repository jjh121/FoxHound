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
    }
}