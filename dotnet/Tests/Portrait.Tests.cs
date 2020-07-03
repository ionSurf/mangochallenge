using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;
using Moq;
using Repositories;
using Xunit;

namespace Tests
{
    public class PortraitTests
    {
        [Fact]
        public void GetAllPortraits_ReturnsListOfPortraits()
        {
            // Arrange
            var mockRepo = new Mock<IPortraitRepository>();
            mockRepo.Setup(repo => (repo.GetAllPortraits())).Returns(GetPortraits());

            // Act
            var result = mockRepo.Object.GetAllPortraits().ToList();

            // Assert
            Assert.IsType<List<Portrait>>(result);
            Assert.Single(result);
        }

        public List<Portrait> GetPortraits()
        {
            return new List<Portrait>
            {
                new Portrait
                {
                    Id = Guid.NewGuid(),
                    Title = "John Keen",
                    Description = "Lorem Ipsum",
                    URL = "media/image.png"
                }
            };
        }
    }
}
