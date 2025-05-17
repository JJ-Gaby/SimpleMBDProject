using System.Data;
using MySql.Data.MySqlClient;

namespace SimpleMDB;

public class MySqlActorMovieRepository : IActorMovieRepository
{
    private string connectionString;

    public MySqlActorMovieRepository(string connectionString)
    {
        this.connectionString = connectionString;
        //Init();
    }

    private void Init()
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS ActorMovies
        (
            id INT AUTO_INCREMENT PRIMARY KEY,
            actorId int NOT NULL,
            movieId int NOT NULL,
            rolename NVARCHAR(64),
            FOREIGN KEY(actorId) REFERENCES Actors(id) ON DELETE CASCADE,
            FOREIGN KEY(movieId) REFERENCES Movies(id) ON DELETE CASCADE
        )
        ";

        cmd.ExecuteNonQuery();//cualquier cmd que no sea select
    }

    public MySqlConnection OpenDb()
    {
        var dbc = new MySqlConnection(connectionString);
        dbc.Open();
        return dbc;
    }

    public async Task<PageResult<(ActorMovie, Movie)>> ReadAllMoviesByActor(int actorId, int page, int size)
    {
        using var dbc = OpenDb();

        using var countCmd = dbc.CreateCommand();
        countCmd.CommandText = @"
            SELECT * 
            FROM ActorMovies AS am
            JOIN Movies AS m ON am.movieId 
            ";

        countCmd.Parameters.AddWithValue("@actorId", actorId);
        int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
            SELECT * 
            FROM ActorMovies AS am
            JOIN Movies AS m ON am.movieId = m.id
            WHERE am.actorId = @actorId
            LIMIT @offset, @limit
        ";
        cmd.Parameters.AddWithValue("@actorId", actorId);
        cmd.Parameters.AddWithValue("@offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@limit", size);

        using var rows = await cmd.ExecuteReaderAsync();

        var amms = new List<(ActorMovie, Movie)>();
        while (await rows.ReadAsync())
        {
            ActorMovie am = new ActorMovie
            (
            rows.GetInt32(0),//id
            rows.GetInt32(1),//actorId
            rows.GetInt32(2),//movieId
            rows.GetString(3)//rolename
            );

            Movie m = new Movie
            (
            rows.GetInt32(4),//id
            rows.GetString(5),//title
            rows.GetInt32(6),//year
            rows.GetString(7),//description
            rows.GetFloat(8)//rating
            );

            amms.Add((am, m));
        }
        return new PageResult<(ActorMovie, Movie)>(amms, totalCount);
    }
    public async Task<PageResult<(ActorMovie, Actor)>> ReadAllActorsByMovie(int movieId, int page, int size)
    {
        using var dbc = OpenDb();

        using var countCmd = dbc.CreateCommand();
        countCmd.CommandText = @"
       SELECT COUNT(*)
        FROM ActorMovies AS am 
        JOIN Actors AS a ON am.actorId = a.id
        WHERE am.movieId = @movieId
        ";

        countCmd.Parameters.AddWithValue("@movieId", movieId);

        int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
            SELECT * 
            FROM ActorMovies AS am
            JOIN Actors AS a ON am.actorId = a.id
            WHERE am.movieId = @movieId
            LIMIT @offset, @limit
        ";
        cmd.Parameters.AddWithValue("@movieId", movieId);
        cmd.Parameters.AddWithValue("@offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@limit", size);

        using var rows = await cmd.ExecuteReaderAsync();

        var amas = new List<(ActorMovie, Actor)>();

        while (await rows.ReadAsync())
        {
            ActorMovie am = new ActorMovie
            (
            rows.GetInt32(0),//id
            rows.GetInt32(1),//actorId
            rows.GetInt32(2),//movieId
            rows.GetString(3)//rolename
            );

            Actor a = new Actor
            (
                rows.GetInt32(4),//id
                rows.GetString(5),//firstname
                rows.GetString(6),//lastname
                rows.GetString(7),//bio
                rows.GetFloat(8)//rating
            );

            amas.Add((am, a));
        }
        return new PageResult<(ActorMovie, Actor)>(amas, totalCount);
    }
    public async Task<List<Actor>> ReadAllActors()
    {
        using var dbc = OpenDb();

        using var countCmd = dbc.CreateCommand();
        countCmd.CommandText = @"
            SELECT COUNT(*)
            FROM Actors
            ";

        int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
            SELECT * 
            FROM Actors
            LIMIT @offset, @limit
        ";

        using var rows = await cmd.ExecuteReaderAsync();

        var actors = new List<Actor>();

        while (await rows.ReadAsync())
        {
            Actor a = new Actor
            (
                rows.GetInt32(0),//id
                rows.GetString(1),//firstname
                rows.GetString(2),//lastname
                rows.GetString(3),//bio
                rows.GetFloat(4)//rating
            );

            actors.Add(a);
        }
        return actors;
    }
    public async Task<List<Movie>> ReadAllMovies()
    {
        using var dbc = OpenDb();

        using var countCmd = dbc.CreateCommand();
        countCmd.CommandText = @"
            SELECT COUNT(*)
            FROM Movies
            ";

        int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
            SELECT * 
            FROM Movies
            LIMIT @offset, @limit
        ";
        

        using var rows = await cmd.ExecuteReaderAsync();

        var movies = new List<Movie>();

        while (await rows.ReadAsync())
        {
            Movie m = new Movie
            (
                rows.GetInt32(0),//id
                rows.GetString(1),//title
                rows.GetInt32(2),//year
                rows.GetString(3),//description
                rows.GetFloat(4)//rating
            );

            movies.Add(m);
        }
        return movies;
    }

    public async Task<ActorMovie?> Create(int actorId, int movieId, string roleName)
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO ActorMovies (actorId, movieId, rolename)
            VALUES (@actorId, @movieId, @rolename);
            SELECT LAST_INSERT_ID();
            ";
        cmd.Parameters.AddWithValue("@actorId", actorId);
        cmd.Parameters.AddWithValue("@movieId", movieId);
        cmd.Parameters.AddWithValue("@rolename", roleName);

        int id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        var actorMovie = new ActorMovie(id, actorId, movieId, roleName);

        return actorMovie;

    }
    public async Task<ActorMovie?> Delete(int id)
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
            DELETE FROM ActorMovies WHERE id = @id;
            ";
        cmd.Parameters.AddWithValue("@id", id);

        var actorMovie = await Read(id);

        return (await cmd.ExecuteNonQueryAsync() > 0) ? actorMovie : null;

    }

    public async Task<ActorMovie?> Read(int id)
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
            SELECT *
            FROM ActorMovies
            WHERE id = @id
            ";
        cmd.Parameters.AddWithValue("@id", id);

        using var rows = await cmd.ExecuteReaderAsync();
        var actors = new List<Actor>();

        if (await rows.ReadAsync())
        {
            return new ActorMovie(
                rows.GetInt32(0),//id
                rows.GetInt32(1),//actorId
                rows.GetInt32(2),//movieId
                rows.GetString(3)//rolename
            );
        }
        return null;
    }
}