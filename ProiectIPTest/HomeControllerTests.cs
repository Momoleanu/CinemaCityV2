using Xunit;
using ProiectIP.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectIP.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace YourNamespace.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(null);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(null);

            // Act
            var result = controller.Privacy() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}