SELECT Actors.Id, Actors.FullName
FROM Actors
JOIN Actors_Movies ON Actors.Id = Actors_Movies.ActorId
JOIN Movies ON Actors_Movies.MovieId = Movies.Id
WHERE Movies.Id = 10;

