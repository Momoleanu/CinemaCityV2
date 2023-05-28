using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Controllers;
using ProiectIP.Data;
using ProiectIP.Models;

[Collection("ActorsControllerTests")]
public class ActorsControllerTests
{
    // Defineți variabilele și instanțele necesare pentru teste

    private readonly DbContextOptions<AppDbContext> _options;

    public ActorsControllerTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [Fact]
    public void Actor_ReturnsViewWithActorDetails()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Creați o bază de date de memorie și adăugați datele necesare pentru testare
        using (var context = new AppDbContext(options))
        {
            // Adăugați datele necesare în baza de date de memorie
            // Aceasta poate include adăugarea de actori pentru a fi utilizați în testul dumneavoastră

            // Exemplu de adăugare a unui actor în baza de date de memorie
            var actor = new Actor { Id = 1, FullName = "John Doe" };
            context.Actors.Add(actor);
            context.SaveChanges();
        }

        using (var context = new AppDbContext(options))
        {
            // Act
            var controller = new ActorsController(context);
            var result = controller.Actor(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<Actor>(result.Model);
            // Verificați că modelul returnat conține detaliile actorului cu ID-ul 1
            Assert.Equal(1, (result.Model as Actor).Id);
        }
    }

    [Fact]
    public void Actor_ReturnsViewResult_WithActorData()
    {
        // Arrange
        using (var context = new AppDbContext(_options))
        {
            var actor = new Actor
            {
                Id = 1,
                PictureURL = "https://example.com/actor1.jpg",
                FullName = "John Doe",
                Bio = "Lorem ipsum dolor sit amet.",
                Actors_Movies = new List<Actor_Movie>()
            };

            context.Actors.Add(actor);
            context.SaveChanges();

            var controller = new ActorsController(context);

            // Act
            var result = controller.Actor(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

            var model = result.Model as Actor;
            Assert.NotNull(model);
            Assert.Equal(actor.Id, model.Id);
            Assert.Equal(actor.PictureURL, model.PictureURL);
            Assert.Equal(actor.FullName, model.FullName);
            Assert.Equal(actor.Bio, model.Bio);
        }
    }
}