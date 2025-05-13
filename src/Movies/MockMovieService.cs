namespace SimpleMDB;

public class MockMovieService : IMovieService
{
  private IMovieRepository movieRepository;

  public MockMovieService(IMovieRepository movieRepository)
  {
    this.movieRepository = movieRepository;
  }
  public async Task<Result<PageResult<Movie>>> ReadAll(int page, int size)
  {
    var pagedResult = await movieRepository.ReadAll(page, size);
    Result<PageResult<Movie>> result = (pagedResult == null) ?
        new Result<PageResult<Movie>>(new Exception("No movies found")) :
        new Result<PageResult<Movie>>(pagedResult);

    return await Task.FromResult(result);
  }
  public async Task<Result<Movie>> Create(Movie newMovie)
  {
    if (string.IsNullOrEmpty(newMovie.Title))
    {
      return new Result<Movie>(new Exception("Title cannot be empty."));
    }
    else if (newMovie.Title.Length > 200)
    {
      return new Result<Movie>(new Exception("Title cannot have more than 200 characters"));
    }
    else if (newMovie.Year > DateTime.Now.Year)
    {
      return new Result<Movie>(new Exception("Year cannot be greater than current year"));
    }
    else if (newMovie.Year > DateTime.Now.Year)
    {
      return new Result<Movie>(new Exception("Year cannot be greater than 2025"));
    }

    Movie? createdMovie = await movieRepository.Create(newMovie);
    var result = (createdMovie == null) ?
    new Result<Movie>(new Exception("Movie not created")) :
    new Result<Movie>(createdMovie);

    return await Task.FromResult(result);

  }
  public async Task<Result<Movie>> Read(int id)
  {
    Movie? movie = await movieRepository.Read(id);

    var result = (movie == null) ?
      new Result<Movie>(new Exception("Movie could not be read.")) :
      new Result<Movie>(movie);

    return await Task.FromResult(result);
  }
  public async Task<Result<Movie>> Update(int id, Movie newMovie)
  {
    if (string.IsNullOrEmpty(newMovie.Title))
    {
      return new Result<Movie>(new Exception("Title cannot be empty."));
    }
    else if (newMovie.Title.Length > 200)
    {
      return new Result<Movie>(new Exception("Title cannot have more than 200 characters"));
    }
    else if (newMovie.Year > DateTime.Now.Year)
    {
      return new Result<Movie>(new Exception("Year cannot be greater than current year"));
    }
    else if (newMovie.Year > DateTime.Now.Year)
    {
      return new Result<Movie>(new Exception("Year cannot be greater than 2025"));
    }
    
    Movie? movie = await movieRepository.Update(id, newMovie);
    var result = (movie == null) ?
      new Result<Movie>(new Exception("Movie could not be updated.")) :
      new Result<Movie>(movie);

    return await Task.FromResult(result);
  }
  public async Task<Result<Movie>> Delete(int id)
  {
    Movie? movie = await movieRepository.Delete(id);
    var result = (movie == null) ?
      new Result<Movie>(new Exception("Movie could not be deleted.")) :
      new Result<Movie>(movie);

    return await Task.FromResult(result);
  }
}