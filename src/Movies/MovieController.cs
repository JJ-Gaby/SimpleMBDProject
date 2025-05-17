namespace SimpleMDB;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public class MovieController
{
    private IMovieService movieService;

    public MovieController(IMovieService movieService)
    {
        this.movieService = movieService;
    }

    //GET /movies?page=1&size=5
    public async Task ViewAllMoviesGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
      string message = req.QueryString["message"] ?? "";
      int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
      int size = int.TryParse(req.QueryString["size"], out int s) ? s : 5;

      Result<PageResult<Movie>> result = await movieService.ReadAll(page, size);
      if (result.IsValid)
      {
        PageResult<Movie> pagedResult = result.Value!;
        List<Movie> movies = pagedResult.Values;
        int movieCount = pagedResult.TotalCount;	

        string html = MovieHtmlTemplates.ViewAllMoviesGet(movies, page, size, movieCount);
        string content = HtmlTemplates.Base("SimpleMDB", "Movies View All Page", html, message);
        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }
      else{
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/");
      }
    }

// GET /movies/add
      public async Task AddMovieGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
      {
      string title = req.QueryString["title"] ?? "";
        string year = req.QueryString["year"] ?? "";
        string description = req.QueryString["description"] ?? "";
        string rating = req.QueryString["rating"] ?? "";
        string message = req.QueryString["message"] ?? "";

        string html = MovieHtmlTemplates.AddMovieGet(title, year, description, rating);
        string content = HtmlTemplates.Base("SimpleMDB", "Movies Add Page", html, message);

        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }

  // POST /movies/add
  public async Task AddMoviePost(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
  {
    var formData = (NameValueCollection?) options["req.form"] ?? [];

    string title = formData["title"] ?? "";
    int year = int.TryParse(formData["year"], out int y) ? y : DateTime.Now.Year;
    string description = formData["description"] ?? "";
    float rating = float.TryParse(formData["rating"], out float r)? r : 5F;

  Console.WriteLine($"title={title}");
  
  Movie newMovie = new Movie(0, title, year, description, rating);

    Result<Movie> result = await movieService.Create(newMovie);
    if (result.IsValid)
    {
      HttpUtils.AddOptions(options, "redirect", "message", "Movie created successfully!");

      await HttpUtils.Redirect(req, res, options, "/movies");
    } 
    else 
    {
      options["message"] = result.Error!.Message;
      HttpUtils.AddOptions(options, "redirect", "title", title);
      HttpUtils.AddOptions(options, "redirect", formData);

      await HttpUtils.Redirect(req, res, options, "/movies/add");
    }
  }

  // GET /movies/view?mid=1
  public async Task ViewMovieGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
      string message = req.QueryString["message"] ?? "";

      int mid = int.TryParse(req.QueryString["mid"], out int u) ? u : 1;
      

      Result<Movie> result = await movieService.Read(mid);
      if (result.IsValid)
      {
        Movie movie = result.Value!;
        string html = MovieHtmlTemplates.ViewMovieGet(movie);
        string content = HtmlTemplates.Base("SimpleMDB", "Movies View Page", html, message);
        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }
      else
      {
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/movies");
      }
    }
  
  // GET /movies/edit?mid=1
   public async Task EditMovieGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
      {
        string message = req.QueryString["message"] ?? "";
        int mid = int.TryParse(req.QueryString["mid"], out int u) ? u : 1;
      

      Result<Movie> result = await movieService.Read(mid);
      if (result.IsValid)
      {
        Movie movie = result.Value!;
        string html = MovieHtmlTemplates.EditMovieGet(movie, mid);
        string content = HtmlTemplates.Base("SimpleMDB", "Movies Edit Page", html, message);
        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }
      else{
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/movies");
      }
      }

  // POST /movies/edit?mid=1
  public async Task EditMoviePost(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
  {
    int mid = int.TryParse(req.QueryString["mid"], out int u) ? u : 0;

    var formData = (NameValueCollection?) options["req.form"] ?? [];

    string title = formData["title"] ?? "";
    int year = int.TryParse(formData["year"], out int y) ? y : DateTime.Now.Year;
    string description = formData["description"] ?? "";
    float rating = float.TryParse(formData["rating"], out float r)? r : 5F;

  Console.WriteLine($"title={title}");
  
  Movie newMovie = new Movie(0, title, year, description, rating);

    Result<Movie> result = await movieService.Update(mid, newMovie);
    if (result.IsValid)
    {
      HttpUtils.AddOptions(options, "redirect", "message","Movie updated successfully!");
      await HttpUtils.Redirect(req, res, options, "/movies");
    } 
    else 
    {
      HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
      await HttpUtils.Redirect(req, res, options, $"/movies/edit?mid={mid}");
    }
  }

// POST /movies/remove?mid=1
public async Task RemoveMoviePost(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
      string message = req.QueryString["message"] ?? "";

      int mid = int.TryParse(req.QueryString["mid"], out int u) ? u : 1;

      Result<Movie> result = await movieService.Delete(mid);
      if (result.IsValid)
      {
        HttpUtils.AddOptions(options, "redirect", "message","Movie edited successfully!");
        await HttpUtils.Redirect(req, res, options, "/movies");
      }
      else{
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/movies");
      }
    }
}