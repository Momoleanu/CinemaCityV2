using Xunit;
using ProiectIP.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProiectIP.Models;
using ProiectIP.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace YourNamespace.Tests
{
    public class MoviesControllerTests
    {
        [Fact]
        public async void Index_ReturnsViewResult_WithListOfMovies()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new MoviesController(context, null);

                context.Movies.AddRange(new List<Movie>
                {
                    new Movie { Id = 1, Title = "Movie 1" },
                    new Movie { Id = 2, Title = "Movie 2" },
                    new Movie { Id = 3, Title = "Movie 3" }
                });
                await context.SaveChangesAsync();

                // Act
                var result = await controller.Index() as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var model = result.Model as List<Movie>;
                Assert.NotNull(model);
                Assert.Equal(3, model.Count);
                Assert.Equal("Movie 1", model[0].Title);
                Assert.Equal("Movie 2", model[1].Title);
                Assert.Equal("Movie 3", model[2].Title);
            }
        }

      

        [Fact]
        public void Buy_ReturnsRedirectToAction_WhenMovieIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new MoviesController(context, null);

                // Act
                var result = controller.Buy("Nonexistent Movie", "10.00", 2, "test@example.com", true) as NotFoundResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<NotFoundResult>(result);
            }
        }


        [Fact]
        public void Success_ReturnsViewResult()
        {
            // Arrange
            var controller = new MoviesController(null, null);

            // Act
            var result = controller.Success() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}