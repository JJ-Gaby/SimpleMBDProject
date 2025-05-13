namespace SimpleMDB;

public class UserHtmlTemplates
{
   public static string ViewAllUsersGet(List<User> users, int page, int size, int userCount)
   {
        int pageCount = (int)Math.Ceiling((double)userCount / size);

        string rows = "";

        foreach (var user in users)
        {
            rows += @$"
            <tr>
           <td> {user.Id} </td>
            <td>{user.Username}</td>
            <td>{user.Password}</td>
            <td>{user.Salt}</td>
            <td>{user.Role}</td>
            <td><a href=""/users/view?uid={user.Id}"">View</a></td>
            <td><a href=""/users/edit?uid={user.Id}"">Edit</a></td>
            <td><form action=""/users/remove?uid={user.Id}"" method= ""POST"" onsubmit=""return confirm('Are you sure you want to remove this user?');"">
            <input type= ""submit"" value=""Remove"">
            </form>
            </td>
            </tr>";
        }

        string html = $@"
        <div class=""add"">
            <a href=""/users/add"">Add New User</a>
        </div>
        <table class= ""viewall"">
        <thead>
            <tr>
                <th>Id</th>
                <th>Username</th>
                <th>Password</th>
                <th>Salt</th>
                <th>Role</th>
                <th>View</th>
                <th>Edit</th>
                <th>Remove</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
        </table>
        <div class=""pagination"">
          <a href=""?page=1&size={size}"">First</a>
          <a href=""?page={page - 1}&size={size}"">Previous</a>
          <span>Page {page} of {pageCount}</span>  
          <a href=""?page={page + 1}&size={size}"">Next</a>
          <a href=""?page={pageCount}&size={size}"">Last</a>
        </div>
";
return html;
   }

   public static string AddUserGet(string username, string role)
   {
           string roles = "";

        foreach(var r in Roles.ROLES)
        {
          string selected = (r == role) ? " selected" : "";
            roles += $@"<option value=""{r}""{selected}>{r}</option>";
        }


        string html = $@"
        <form class=""addform"" action=""/users/add"" method=""POST"">
            <label for=""username"">Username</label>
            <input type=""text"" id=""username"" name=""username"" placeholder= ""Username"" value =""{username}"">
            <label for=""password"">Password</label>
            <input type=""password"" id=""password"" name=""password""  placeholder= ""Password"">
            <label for=""role"">Role</label>
            <select id=""role"" name=""role"">
                {roles}
            </select>
            <input type=""submit"" value=""Add User"">
        </form>";

        return html;
   } 

   public static string ViewUserGet(User user){
            string html = $@"
        <table class=  ""view"">
        <thead>
            <tr>
                <th>Id</th>
                <th>Username</th>
                <th>Password</th>
                <th>Salt</th>
                <th>Role</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>{user.Id}</td>
                <td>{user.Username}</td>
                <td>{user.Password}</td>
                <td>{user.Salt}</td>
                <td>{user.Role}</td>
            </tr>
        </tbody>
        </table>";
        return html;
   }

   public static string EditUserGet(User user, int uid)
   {
            string roles = "";

        foreach(var role in Roles.ROLES)
        {
          string selected = (role == user.Role) ? "selected" : "";
            roles += $@"<option value=""{role}""{selected}>{role}</option>";
        }

        string html = $@"
        <form class=""editform"" action=""/users/edit?uid={uid}"" method=""POST"">
            <label for=""username"">Username:</label>
            <input type=""text"" id=""username"" name=""username"" placeholder= ""Username"" value =""{user.Username}""><br><br>
            <label for=""password"">Password:</label>
            <input type=""password"" id=""password"" name=""password"" placeholder= ""Password""value =""{user.Password}""><br><br>
            <label for=""role"">Role:</label>
            <select id=""role"" name=""role"">
            {roles}
            </select>
            <input type=""submit"" value=""Edit"">
        </form>";
        return html;
   }
}