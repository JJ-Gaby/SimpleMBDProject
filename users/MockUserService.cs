namespace SimpleMDB;

public class MockUserService : IUserService
{
  private IUserRepository userRepository;

  public MockUserService(IUserRepository userRepository)
  {
    this.userRepository = userRepository;
  }
  public async Task<Result<PageResult<User>>> ReadAll(int page, int size)
  {
    var pagedResult = await userRepository.ReadAll(page, size);
    Result<PageResult<User>> result = (pagedResult == null) ?
        new Result<PageResult<User>>(new Exception("No users found")) :
        new Result<PageResult<User>>(pagedResult);

    return await Task.FromResult(result);
  }
  public async Task<Result<User>> Create(User newUser)
  {
    if (string.IsNullOrEmpty(newUser.Username))
    {
      return new Result<User>(new Exception("Username cannot be empty."));
    }
    else if (newUser.Username.Length > 16)
    {
      return new Result<User>(new Exception("Username cannot have more than 16 characters"));
    }
    User? createdUser = await userRepository.Create(newUser);
    var result = (createdUser == null) ?
    new Result<User>(new Exception("User not created")) :
    new Result<User>(createdUser);

    return await Task.FromResult(result);

  }
  public async Task<Result<User>> Read(int id)
  {
    User? user = await userRepository.Read(id);

    var result = (user == null) ?
      new Result<User>(new Exception("User could not be read.")) :
      new Result<User>(user);

    return await Task.FromResult(result);
  }
  public async Task<Result<User>> Update(int id, User newUser)
  {
     if (string.IsNullOrEmpty(newUser.Username))
    {
      return new Result<User>(new Exception("Username cannot be empty."));
    }
    else if (newUser.Username.Length > 16)
    {
      return new Result<User>(new Exception("Username cannot have more than 16 characters"));
    }
    User? user = await userRepository.Update(id, newUser);
    var result = (user == null) ?
      new Result<User>(new Exception("User could not be updated.")) :
      new Result<User>(user);

    return await Task.FromResult(result);
  }
  public async Task<Result<User>> Delete(int id)
  {
    User? user = await userRepository.Delete(id);
    var result = (user == null) ?
      new Result<User>(new Exception("User could not be deleted.")) :
      new Result<User>(user);

    return await Task.FromResult(result);
  }
}