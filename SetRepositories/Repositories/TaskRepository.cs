using Microsoft.EntityFrameworkCore;
using ToDoListApi.Contexts;
using ToDoListApi.Entities;
using ToDoListApi.SetRepositories.IRepositories;

namespace ToDoListApi.SetRepositories.Repositories;

public class TaskRepository : ITasksRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Tasks> CreateAsync(Tasks tasks)
    {
        try
        {
            if (tasks is null)
                throw new ArgumentNullException(nameof(tasks));

            await _context.Tasks.AddAsync(tasks);

            return tasks;
        }
        catch (Exception e)
        {
            throw new Exception($"Error:{e}");
        }
    }

    public async Task<Tasks> DeleteAsync(ulong Id)
    {
        try
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            Tasks task = await _context.Tasks.FirstOrDefaultAsync(u => u.Id == Id);

            if (task is null)
                return null;

            this._context.Tasks.Remove(task);

            return task;
        }
        catch (Exception e)
        {
            throw new Exception($"{e}");
        }
    }

    public async Task<List<Tasks>> GetAllAsync(ulong Id)
    {
        try
        {
            if (Id == 0)
                throw new ArgumentNullException("Id is required");

            List<Tasks> taskList = await this._context.Tasks
                .AsNoTracking().Where(t => t.FkUserId == Id).ToListAsync();

            return taskList;
        }
        catch (Exception e)
        {
            throw new Exception($"{e}");
        }
    }

    public async Task<Tasks> GetAsync(ulong Id)
    {
        try
        {
            if (Id == 0)
                throw new ArgumentNullException("Id is required");

            Tasks task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(u => u.Id == Id);

            return task;
        }
        catch (Exception e)
        {
            throw new Exception($"{e}");
        }
    }

    public async Task<Tasks> UpdateAsync(Tasks tasks)
    {
        try
        {
            if (tasks is null)
                throw new ArgumentNullException(nameof(tasks));

            Tasks taskFound = await this._context.Tasks.AsNoTracking().FirstOrDefaultAsync(u => u.Id == tasks.Id);

            if (taskFound is null)
                return null;

            _context.Entry(tasks).State = EntityState.Modified;

            return tasks;
        }
        catch (Exception e)
        {
            throw new Exception($"{e}");
        }
    }

    public async Task<bool> DeleteAllAsync(ulong Id)
    {
        try
        {
            if (Id == 0)
                throw new ArgumentNullException("Id is required");

            List<Tasks> taskList = await this._context.Tasks
                .AsNoTracking().Where(t => t.FkUserId == Id).ToListAsync();

            this._context.Tasks.RemoveRange(taskList);

            return true;
        }
        catch (Exception e)
        {
            throw new Exception($"{e}");
        }
    }

    public async Task<Tasks> AlterStatusDoneAsync(ulong Id)
    {
        try
        {
            if (Id == 0)
                throw new ArgumentNullException("Id is required");

            Tasks task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(u => u.Id == Id);

            if (task is null)
                throw new ArgumentNullException("Task not found");

            task.IsDone = !task.IsDone;

            _context.Entry(task).State = EntityState.Modified;

            return task;
        }
        catch (Exception e)
        {
            throw new Exception($"{e}");
        }
    }


}