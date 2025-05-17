using System.Data;
using MySql.Data.MySqlClient;

namespace SimpleMDB;

public class MySqlMovieRepository : IMovieRepository
{
    private string connectionString;

    public MySqlMovieRepository(string connectionString)
    {
        this.connectionString = connectionString;
        //Init();
    }

    private void Init()
    {
     using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS Movies
        (
            id INT AUTO_INCREMENT PRIMARY KEY,
            title NVARCHAR(256) NOT NULL,
            year int NOT NULL,
            description NVARCHAR(4096),
            rating FLOAT
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

    public async Task<PageResult<Movie>> ReadAll(int page, int size)
    {
        using var dbc = OpenDb();

        using var countCmd = dbc.CreateCommand();
        countCmd.CommandText = "SELECT COUNT(*) FROM Movies";
        int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());//Scalar es un select que devuelve UN valor

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = "SELECT * FROM Movies LIMIT @offset, @limit";
        cmd.Parameters.AddWithValue("@offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@limit", size);

        using var rows = await cmd.ExecuteReaderAsync();//reader para formar o buscar respuesta
        var actors = new List<Movie>();

        while (await rows.ReadAsync())
        {
            actors.Add(new Movie
            {
                Id = rows.GetInt32("id"),
                Title = rows.GetString("title"),
                Year = rows.GetInt32("year"),
                Description = rows.GetString("description"),
                Rating = rows.GetFloat("rating")
            });
        }

        return new PageResult<Movie>(actors, totalCount);
    }
    public async Task<Movie?> Create(Movie actor)
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
        INSERT INTO Movies (title, year, description, rating)
        VALUES(@title, @year, @description, @rating); 
        SELECT LAST_INSERT_ID();";
        cmd.Parameters.AddWithValue("@title", actor.Title);
        cmd.Parameters.AddWithValue("@year", actor.Year);
        cmd.Parameters.AddWithValue("@description", actor.Description);
        cmd.Parameters.AddWithValue("@rating", actor.Rating);

        actor.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());

        return actor;
    }
    public async Task<Movie?> Read(int id)
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = "SELECT * FROM Movies WHERE id = @id;";
        cmd.Parameters.AddWithValue("@id", id);

        using var rows = await cmd.ExecuteReaderAsync();

        if (await rows.ReadAsync())
        {
            return new Movie
            {
                Id = rows.GetInt32("id"),
                Title = rows.GetString("title"),
                Year = rows.GetInt32("year"),
                Description = rows.GetString("description"),
                Rating = rows.GetInt32("rating")
            };
        }
        return null;
    }
    public async Task<Movie?> Update(int id, Movie newMovie)
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = @"
        UPDATE Movies SET
        title = @title, 
        year = @year, 
        description = @description, 
        rating = @rating 
        WHERE id = @id;";
        cmd.Parameters.AddWithValue("@title", newMovie.Title);
        cmd.Parameters.AddWithValue("@year", newMovie.Year);
        cmd.Parameters.AddWithValue("@description", newMovie.Description);
        cmd.Parameters.AddWithValue("@rating", newMovie.Rating);
        cmd.Parameters.AddWithValue("@id", id);

        return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0 ? newMovie : null;
    }
    public async Task<Movie?> Delete(int id)
    {
        using var dbc = OpenDb();

        using var cmd = dbc.CreateCommand();
        cmd.CommandText = "DELETE FROM Movies WHERE id = @id;";
        cmd.Parameters.AddWithValue("@id", id);

        Movie? actor = await Read(id);

        return Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0 ? actor : null;
    }
}