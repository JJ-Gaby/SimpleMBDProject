namespace SimpleMDB;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public class UserController
{
    private IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    //GET /users?page=1&size=5
    public async Task ViewAllUsersGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
      string message = req.QueryString["message"] ?? "";
      int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
      int size = int.TryParse(req.QueryString["size"], out int s) ? s : 5;

      Result<PageResult<User>> result = await userService.ReadAll(page, size);
      if (result.IsValid)
      {
        PageResult<User> pagedResult = result.Value!;
        List<User> users = pagedResult.Values;
        int userCount = pagedResult.TotalCount;	

        string html = UserHtmlTemplates.ViewAllUsersGet(users, page, size, userCount);
        string content = HtmlTemplates.Base("SimpleMDB", "Users View All Page", html, message);
        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }
      else{
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/");
      }
    }

// GET /users/add
      public async Task AddUserGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
      {
      string username = req.QueryString["username"] ?? "";
        string password = req.QueryString["password"] ?? "";
        string role = req.QueryString["role"] ?? "";
        string message = req.QueryString["message"] ?? "";
        string html = UserHtmlTemplates.AddUserGet(username, role);
        string content = HtmlTemplates.Base("SimpleMDB", "Users Add Page", html, message);
        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }

  // POST /users/add
  public async Task AddUserPost(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
  {
    var formData = (NameValueCollection?) options["req.form"] ?? [];

    string username = formData["username"] ?? "";
    string password = formData["password"] ?? "";
    string role = formData["role"] ?? "";

  Console.WriteLine($"username={username}");
  
  User newUser = new User(0, username, password, "", role);

    Result<User> result = await userService.Create(newUser);
    if (result.IsValid)
    {
      HttpUtils.AddOptions(options, "redirect", "message", "User created successfully!");

      await HttpUtils.Redirect(req, res, options, "/users");
    } 
    else 
    {
      options["message"] = result.Error!.Message;
      HttpUtils.AddOptions(options, "redirect", "username", username);
      HttpUtils.AddOptions(options, "redirect", "role", role);

      await HttpUtils.Redirect(req, res, options, "/users/add");
    }
  }

  // GET /users/view?uid=1
  public async Task ViewUserGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
      string message = req.QueryString["message"] ?? "";

      int uid = int.TryParse(req.QueryString["uid"], out int u) ? u : 1;
      

      Result<User> result = await userService.Read(uid);
      if (result.IsValid)
      {
        User user = result.Value!;
        string html = UserHtmlTemplates.ViewUserGet(user);
        string content = HtmlTemplates.Base("SimpleMDB", "Users View Page", html, message);
        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }
      else
      {
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/users");
      }
    }
  
  // GET /users/edit?uid=1
   public async Task EditUserGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
      {
        string message = req.QueryString["message"] ?? "";
        int uid = int.TryParse(req.QueryString["uid"], out int u) ? u : 1;
      

      Result<User> result = await userService.Read(uid);
      if (result.IsValid)
      {
        User user = result.Value!;
        string html = UserHtmlTemplates.EditUserGet(user, uid);
        string content = HtmlTemplates.Base("SimpleMDB", "Users Edit Page", html, message);
        await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.OK, content);
      }
      else{
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/users");
      }
      }

  // POST /users/edit?uid=1
  public async Task EditUserPost(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
  {
    int uid = int.TryParse(req.QueryString["uid"], out int u) ? u : 0;

    var formData = (NameValueCollection?) options["req.form"] ?? [];

    string username = formData["username"] ?? "";
    string password = formData["password"] ?? "";
    string role = formData["role"] ?? "";

  Console.WriteLine($"username={username}");
  
  User newUser = new User(0, username, password, "", role);

    Result<User> result = await userService.Update(uid, newUser);
    if (result.IsValid)
    {
      HttpUtils.AddOptions(options, "redirect", "message","User updated successfully!");
      await HttpUtils.Redirect(req, res, options, "/users");
    } 
    else 
    {
      HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
      await HttpUtils.Redirect(req, res, options, $"/users/edit?uid={uid}");
    }
  }

// POST /users/remove?uid=1
public async Task RemoveUserPost(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
      string message = req.QueryString["message"] ?? "";

      int uid = int.TryParse(req.QueryString["uid"], out int u) ? u : 1;

      Result<User> result = await userService.Delete(uid);
      if (result.IsValid)
      {
        HttpUtils.AddOptions(options, "redirect", "message","User edited successfully!");
        await HttpUtils.Redirect(req, res, options, "/users");
      }
      else{
        HttpUtils.AddOptions(options, "redirect", "message", result.Error!.Message);
        await HttpUtils.Redirect(req, res, options, "/users");
      }
    }
}