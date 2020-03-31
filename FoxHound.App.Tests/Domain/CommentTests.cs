using AutoFixture;
using AutoFixture.AutoMoq;
using FoxHound.App.Domain;
using System;
using Xunit;

namespace FoxHound.App.Tests.Domain
{
    public class CommentTests
    {
        private readonly IFixture _fixture;

        public CommentTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void Constructor_TakesParameters_InitializesCorrectly()
        {
            // Arrange
            var author = _fixture.Create<string>();
            var content = _fixture.Create<string>();

            // Act
            var result = new Comment(author, content);

            // Assert
            Assert.Equal(0, result.CommentId);
            Assert.Equal(0, result.PostId);
            Assert.Equal(author, result.Author);
            Assert.Equal(content, result.Content);
            Assert.True(DateTime.Now >= result.CreatedDate);
        }
    }
}