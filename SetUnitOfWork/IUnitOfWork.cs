using ToDoListApi.SetRepositories.IRepositories;

namespace ToDoListApi.SetUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    ITasksRepository TasksRepository { get; }

    Task Commit();

}