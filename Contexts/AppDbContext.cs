using Microsoft.EntityFrameworkCore;
using ToDoListApi.Entities;

namespace ToDoListApi.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<User>? Users { get; set; }
        public DbSet<Tasks>? Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}