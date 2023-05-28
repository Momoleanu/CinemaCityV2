using Xunit;
using ProiectIP.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectIP.Models;

namespace YourNamespace.Tests
{
    public class RoomsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfRooms()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, Name = "Room 1", Description = "Description 1", NoRow = 5, NoColumn = 10 },
                    new Room { Id = 2, Name = "Room 2", Description = "Description 2", NoRow = 6, NoColumn = 12 }
                });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var controller = new RoomsController(context);

                // Act
                var result = await controller.Index() as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<ViewResult>(result);

                var model = result.Model as List<Room>;
                Assert.NotNull(model);
                Assert.Equal(2, model.Count);
                Assert.Equal("Room 1", model.First().Name);
                Assert.Equal("Room 2", model.Last().Name);
            }

            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();
            }
        }
    }
}