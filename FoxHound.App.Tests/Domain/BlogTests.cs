using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Domain;
using System;
using Xunit;

namespace FoxHound.App.Tests.Domain
{
    public class BlogTests
    {
        private readonly IFixture _fixture;

        public BlogTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void Constructor_TakesParameters_InitializesCorrectly()
        {
            // Arrange
            var title = _fixture.Create<string>();
            var owner = _fixture.Create<string>();

            // Act
            var result = new Blog(title, owner);

            // Assert
            Assert.Equal(0, result.BlogId);
            Assert.Equal(title, result.Title);
            Assert.Equal(owner, result.Owner);
            Assert.True(DateTime.Now >= result.CreatedDate);
            Assert.NotNull(result.Posts);
            Assert.Empty(result.Posts);
        }
    }
}