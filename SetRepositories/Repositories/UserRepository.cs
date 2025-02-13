using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Contexts;
using ToDoListApi.DTOs;
using ToDoListApi.Entities;
using ToDoListApi.SetRepositories.IRepositories;

namespace ToDoListApi.SetRepositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(user);

                user.Email = user.Email.Trim();
                user.Password = user.Password.Trim();

                await this._context.Users.AddAsync(user);

                return user;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error creating user: " + e.Message, e);
            }
        }

        public async Task<User> DeleteAsync(ulong id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentException("Id is required", nameof(id));

                User user = await this._context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return null;

                _context.Users.Remove(user);


                return user;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error deleting user: " + e.Message, e);
            }
        }

        public async Task<User> GetAsync(ulong id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentException("Id is required", nameof(id));

                User user = await this._context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

                return user;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error retrieving user: " + e.Message, e);
            }
        }

        public async Task<bool> LoginAsync(UserLoginDTO dto)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(dto.Email));

                if (user is null)
                    return false;

                if (!dto.Password.Equals(user.Password))
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error logging in: " + e.Message, e);
            }
        }


        public async Task<User> UpdateAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                User userFound = await this._context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);

                if (userFound == null)
                    return null;

                _context.Entry(user).State = EntityState.Modified;

                return user;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error updating user: " + e.Message, e);
            }
        }
    }
}