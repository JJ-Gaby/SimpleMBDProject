using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections;
using SimpleMBD;

namespace SimpleMDB;

public class App
{
    private HttpListener server;
    private HttpRouter router;
    private int requestId;


    public App()
    {
        string host = "http://127.0.0.1:8080/";
        server = new HttpListener();
        server.Prefixes.Add(host);
        requestId = 0;

        Console.WriteLine("Server is listening..." + host);

        //var userRepository = new MockUserRepository();
        var userRepository = new MySqlUserRepository("Server=localhost;Database=simplemdb;Uid=root;Pwd=1234");
        var userService = new MockUserService(userRepository);
        var authController = new AuthController(userService);
        var userController = new UserController(userService);

        //var actorRepository = new MockActorRepository();
        var actorRepository = new MySqlActorRepository("Server=localhost;Database=simplemdb;Uid=root;Pwd=1234");
        var actorService = new MockActorService(actorRepository);
        var actorController = new ActorController(actorService);

        //var movieRepository = new MockMovieRepository();
        var movieRepository = new MySqlMovieRepository("Server=localhost;Database=simplemdb;Uid=root;Pwd=1234");
        var movieService = new MockMovieService(movieRepository);
        var movieController = new MovieController(movieService);

        //var actorMovieRepository = new MockActorMovieRepository(actorRepository, movieRepository);
        var actorMovieRepository = new MySqlActorMovieRepository("Server=localhost;Database=simplemdb;Uid=root;Pwd=1234");
        var actorMovieService = new MockActorMovieService(actorMovieRepository);
        var actorMovieController = new ActorMovieController(actorMovieService, actorService, movieService);


        router = new HttpRouter();
        router.Use(HttpUtils.ServeStaticFile);
        router.Use(HttpUtils.ReadRequestFormData);
        

        router.AddGet("/", authController.LandingPageGet);
        router.AddGet("/register", authController.RegisterGet);
        router.AddPost("/register", authController.RegisterPost);
        router.AddGet("/login", authController.LoginGet);
        router.AddPost("/login", authController.LoginPost);
        router.AddPost("/logout", authController.LogoutPost);

        router.AddGet("/users", authController.CheckAdmin, userController.ViewAllUsersGet);
        router.AddGet("/users/add", authController.CheckAdmin, userController.AddUserGet);
        router.AddPost("/users/add", authController.CheckAdmin, userController.AddUserPost);
        router.AddGet("/users/view", authController.CheckAdmin, userController.ViewUserGet);
        router.AddGet("/users/edit", authController.CheckAdmin, userController.EditUserGet);
        router.AddPost("/users/edit", authController.CheckAdmin, userController.EditUserPost);
        router.AddPost("/users/remove", authController.CheckAdmin, userController.RemoveUserPost);
        
        router.AddGet("/actors", actorController.ViewAllActorsGet);
        router.AddGet("/actors/add", authController.CheckAuth, actorController.AddActorGet);
        router.AddPost("/actors/add", authController.CheckAuth, actorController.AddActorPost);
        router.AddGet("/actors/view", authController.CheckAuth, actorController.ViewActorGet);
        router.AddGet("/actors/edit", authController.CheckAuth, actorController.EditActorGet);
        router.AddPost("/actors/edit", authController.CheckAuth, actorController.EditActorPost);
        router.AddPost("/actors/remove", authController.CheckAuth, actorController.RemoveActorPost);

        router.AddGet("/movies", movieController.ViewAllMoviesGet);
        router.AddGet("/movies/add", authController.CheckAuth, movieController.AddMovieGet);
        router.AddPost("/movies/add", authController.CheckAuth, movieController.AddMoviePost);
        router.AddGet("/movies/view", authController.CheckAuth, movieController.ViewMovieGet);
        router.AddGet("/movies/edit", authController.CheckAuth, movieController.EditMovieGet);
        router.AddPost("/movies/edit", authController.CheckAuth, movieController.EditMoviePost);
        router.AddPost("/movies/remove", authController.CheckAuth, movieController.RemoveMoviePost);

        router.AddGet("/actors/movies", authController.CheckAuth, actorMovieController.ViewAllMoviesByActorGet);
        router.AddGet("/actors/movies/add", authController.CheckAuth, actorMovieController.AddMoviesByActorGet);
        router.AddPost("/actors/movies/add", authController.CheckAuth, actorMovieController.AddMoviesByActorPost);
        router.AddPost("/actors/movies/remove", authController.CheckAuth, actorMovieController.RemoveMoviesByActorPost);
        
        router.AddGet("/movies/actors", authController.CheckAuth, actorMovieController.ViewAllActorsByMovieGet);
        router.AddGet("/movies/actors/add", authController.CheckAuth, actorMovieController.AddActorsByMovieGet);
        router.AddPost("/movies/actors/add", authController.CheckAuth, actorMovieController.AddActorsByMoviePost);
        router.AddPost("/movies/actors/remove", authController.CheckAuth, actorMovieController.RemoveActorsByMoviePost);
        
    }

    public async Task Start()
    {
        server.Start();

        while (server.IsListening)
        {
            var ctx = await server.GetContextAsync();
            _ = HandleContextAsync(ctx);
        }
    }
    public void Stop()
    {
        server.Stop();
        server.Close();
    }

    private async Task HandleContextAsync(HttpListenerContext ctx)
    {
        var req = ctx.Request;
        var res = ctx.Response;
        var options = new Hashtable();

        var rid = req.Headers["X-Request-ID"] ?? requestId.ToString().PadLeft(6, ' ');
        var method = req.HttpMethod;
        var rawUrl = req.RawUrl;
        var remoteEndPoint = req.RemoteEndPoint;

        res.StatusCode = HttpRouter.RESPONSE_NOT_SENT_YET;
        DateTime startTime = DateTime.UtcNow;
        requestId++;
        string error = "";

        try
        {
            await router.Handle(req, res, options);
        }
        catch (Exception ex)
        {
            error = ex.ToString();

            if (res.StatusCode == HttpRouter.RESPONSE_NOT_SENT_YET)
            {
                if (Environment.GetEnvironmentVariable("DEVELOPMENT MODE") != "Production")
                {
                    string html = HtmlTemplates.Base("SimpleMDB", "Error Page", ex.ToString());
                    await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.InternalServerError, html);
                }
                else
                {
                    string html = HtmlTemplates.Base("SimpleMDB", "Error Page", "An error occured. Please try again later.");
                    await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.InternalServerError, html);
                }
            }
        }
        finally
        {
            if (res.StatusCode == HttpRouter.RESPONSE_NOT_SENT_YET)
            {
                string html = HtmlTemplates.Base("SimpleMDB", "Page Not Found", "Resource not found");
                await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.NotFound, html);
            }

            TimeSpan elapsedTime = DateTime.UtcNow - startTime;

            Console.WriteLine($"Request {rid}: {method} {rawUrl} from {req.UserHostName} --> {remoteEndPoint} ({res.ContentLength64} bytes) [{res.ContentType}] in {elapsedTime.TotalMilliseconds}ms error:\"{error}\"");
        }
    }
}