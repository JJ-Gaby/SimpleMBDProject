namespace SimpleMDB;

 public interface IActorMovieRepository
{
    public Task<PageResult<(ActorMovie, Movie)>> ReadAllMoviesByActor(int actorId, int page, int size);
    public Task<PageResult<(ActorMovie, Actor)>> ReadAllActorsByMovie(int movieId, int page, int size);
    public Task<List<Actor>> ReadAllActors();
    public Task<List<Movie>> ReadAllMovies();
    public Task<ActorMovie?> Create(int actorId, int movieId, string roleName);
    public Task<ActorMovie?> Delete(int id);
}
