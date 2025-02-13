using ToDoListApi.DTOs;
using ToDoListApi.Entities;

namespace ToDoListApi.SetRepositories.IRepositories;

public interface IUserRepository
{
    Task<User> GetAsync(ulong id);
    Task<User> DeleteAsync(ulong id);
    Task<User> UpdateAsync(User user);
    Task<User> CreateAsync(User user);
    Task<bool> LoginAsync(UserLoginDTO dto);
}
