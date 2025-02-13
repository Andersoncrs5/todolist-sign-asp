using ToDoListApi.Entities;

namespace ToDoListApi.SetRepositories.IRepositories;

public interface ITasksRepository
{
    Task<Tasks> GetAsync(ulong Id);
    Task<Tasks> AlterStatusDoneAsync(ulong Id);
    Task<List<Tasks>> GetAllAsync(ulong Id);
    Task<bool> DeleteAllAsync(ulong Id);
    Task<Tasks> DeleteAsync(ulong Id);
    Task<Tasks> CreateAsync(Tasks tasks);
    Task<Tasks> UpdateAsync(Tasks tasks);
}
