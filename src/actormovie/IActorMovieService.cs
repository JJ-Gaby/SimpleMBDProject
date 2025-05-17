using SimpleMDB;

namespace SimpleMBD;

  public interface IActorMovieService
  {
    public Task<Result<PageResult<(ActorMovie, Movie)>>> ReadAllMoviesByActor(int actorId, int page, int size);
    public Task<Result<PageResult<(ActorMovie, Actor)>>> ReadAllActorsByMovie(int movieId, int page, int size);
    public Task<Result<List<Actor>>> ReadAllActors();
    public Task<Result<List<Movie>>> ReadAllMovies();
    public Task<Result<ActorMovie>> Create(int actorId, int movieId, string roleName);
    public Task<Result<ActorMovie>> Delete(int id);
  }