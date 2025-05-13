namespace SimpleMDB;

public class MockActorService : IActorService
{
  private IActorRepository actorRepository;

  public MockActorService(IActorRepository actorRepository)
  {
    this.actorRepository = actorRepository;
  }
  public async Task<Result<PageResult<Actor>>> ReadAll(int page, int size)
  {
    var pagedResult = await actorRepository.ReadAll(page, size);
    Result<PageResult<Actor>> result = (pagedResult == null) ?
        new Result<PageResult<Actor>>(new Exception("No actors found")) :
        new Result<PageResult<Actor>>(pagedResult);

    return await Task.FromResult(result);
  }
  public async Task<Result<Actor>> Create(Actor newActor)
  {
    if (string.IsNullOrEmpty(newActor.FirstName))
    {
      return new Result<Actor>(new Exception("First name cannot be empty."));
    }
    else if (newActor.FirstName.Length > 16)
    {
      return new Result<Actor>(new Exception("First name cannot have more than 16 characters"));
    }
    else  if (string.IsNullOrEmpty(newActor.FirstName))
    {
      return new Result<Actor>(new Exception("Last name cannot be empty."));
    }
    else if (newActor.FirstName.Length > 16)
    {
      return new Result<Actor>(new Exception("Last name cannot have more than 16 characters"));
    }


    Actor? createdActor = await actorRepository.Create(newActor);
    var result = (createdActor == null) ?
    new Result<Actor>(new Exception("Actor not created")) :
    new Result<Actor>(createdActor);

    return await Task.FromResult(result);

  }
  public async Task<Result<Actor>> Read(int id)
  {
    Actor? actor = await actorRepository.Read(id);

    var result = (actor == null) ?
      new Result<Actor>(new Exception("Actor could not be read.")) :
      new Result<Actor>(actor);

    return await Task.FromResult(result);
  }
  public async Task<Result<Actor>> Update(int id, Actor newActor)
  {
        if (string.IsNullOrEmpty(newActor.FirstName))
    {
      return new Result<Actor>(new Exception("First name cannot be empty."));
    }
    else if (newActor.FirstName.Length > 16)
    {
      return new Result<Actor>(new Exception("First name cannot have more than 16 characters"));
    }
    else  if (string.IsNullOrEmpty(newActor.FirstName))
    {
      return new Result<Actor>(new Exception("Last name cannot be empty."));
    }
    else if (newActor.FirstName.Length > 16)
    {
      return new Result<Actor>(new Exception("Last name cannot have more than 16 characters"));
    }
    
    Actor? actor = await actorRepository.Update(id, newActor);
    var result = (actor == null) ?
      new Result<Actor>(new Exception("Actor could not be updated.")) :
      new Result<Actor>(actor);

    return await Task.FromResult(result);
  }
  public async Task<Result<Actor>> Delete(int id)
  {
    Actor? actor = await actorRepository.Delete(id);
    var result = (actor == null) ?
      new Result<Actor>(new Exception("Actor could not be deleted.")) :
      new Result<Actor>(actor);

    return await Task.FromResult(result);
  }
}