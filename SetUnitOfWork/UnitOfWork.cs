using ToDoListApi.Contexts;
using ToDoListApi.SetRepositories.IRepositories;
using ToDoListApi.SetRepositories.Repositories;

namespace ToDoListApi.SetUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository? _userRepository;
        private ITasksRepository? _tasksRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public ITasksRepository TasksRepository => _tasksRepository ??= new TaskRepository(_context);

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}