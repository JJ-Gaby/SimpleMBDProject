using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections;

namespace SimpleMDB;

public class App
{
  private HttpListener server;
    private HttpRouter router;

  public App()
{
    string host = "http://127.0.0.1:8080/";
    server = new HttpListener();

    server.Prefixes.Add(host);

    Console.WriteLine("Server is listening..."+host);

    var userRepository = new MockUserRepository();
    var userService = new MockUserService(userRepository);
    var authController = new AuthController(userService);
    var userController = new UserController(userService);

    router = new HttpRouter();
    router.Use(HttpUtils.ReadRequestFormData);

    router.AddGet("/", authController.LandingPageGet);
    router.AddGet("/users", userController.ViewAllGet);
    router.AddGet("/users/add", userController.AddGet);
    router.AddPost("/users/add", userController.AddPost);
    router.AddGet("/users/view", userController.ViewGet);
    router.AddGet("/users/edit", userController.EditGet);
    router.AddPost("/users/edit", userController.EditPost);
    router.AddGet("/users/remove", userController.RemoveGet);
}

public async Task Start()
{
    server.Start();
    while (server.IsListening)
    {
        var ctx = server.GetContext();
        await HandleContextAsync(ctx);
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

    await router.Handle(req, res, options);
}
}