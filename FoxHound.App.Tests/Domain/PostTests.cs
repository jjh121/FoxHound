using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Domain;
using System;
using Xunit;

namespace FoxHound.App.Tests.Domain
{
    public class PostTests
    {
        private readonly IFixture _fixture;

        public PostTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void Constructor_TakesParameters_InitializesCorrectly()
        {
            // Arrange
            var title = _fixture.Create<string>();
            var content = _fixture.Create<string>();

            // Act
            var result = new Post(title, content);

            // Assert
            Assert.Equal(0, result.PostId);
            Assert.Equal(0, result.BlogId);
            Assert.Equal(title, result.Title);
            Assert.Equal(content, result.Content);
            Assert.True(DateTime.Now >= result.CreatedDate);
            Assert.NotNull(result.Comments);
            Assert.Empty(result.Comments);
        }
    }
}