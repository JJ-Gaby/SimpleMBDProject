namespace SimpleMDB;

public class MockUserRepository : IUserRepository
{
    private List<User> users;
    private int idCount;

    public MockUserRepository()
    {
        users = [];
        idCount = 0;

        var usernames = new string[]{
        "Isaac", "Miriam", "Moses", "Aaron", "David", "Solomon", "Elijah", "Isaiah", "Jeremiah", "Ezekiel",
        "Daniel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk", "Zephaniah",
        
        };

        Random r = new Random();

        foreach (var username in usernames)
        {
            var pass = Path.GetRandomFileName();
            var salt = Path.GetRandomFileName();
            var role = Roles.ROLES[r.Next(Roles.ROLES.Length)];
            User user = new User(idCount++, username, pass, salt, role );
            users.Add(user);
    }
    }

    public async Task<PageResult<User>> ReadAll(int page, int size)
    {
     int totalCount = users.Count;
     int start = Math.Clamp((page-1)*size, 0, totalCount);
     int length = Math.Clamp(size, 0, totalCount - start);
     List<User> values = users.Slice(start, length);
     var pagedResult = new PageResult<User>(values, totalCount);

     return await Task.FromResult(pagedResult);
    }
    public async Task<User?> Create(User newUser)
    {
        newUser.Id = idCount++;
        users.Add(newUser);
        

        return await Task.FromResult(newUser);
    }
    public async Task<User?> Read(int id)
    {
        User? user = users.FirstOrDefault((u) => u.Id == id);
        return await Task.FromResult(user);
    }
    public async Task<User?> Update(int id, User newUser)
    {
        User? user = users.FirstOrDefault((u) => u.Id == id);
        if (user != null)
        {
            user.Username = newUser.Username;
            user.Password = newUser.Password;
            user.Salt = newUser.Salt;
            user.Role = newUser.Role;
        }
        return await Task.FromResult(user);
    }    
    public async Task<User?> Delete(int id)
    {
        User? user = users.FirstOrDefault((u) => u.Id == id);
        if (user != null)
        {
            users.Remove(user);
        }
        return await Task.FromResult(user);
    }
    
    }