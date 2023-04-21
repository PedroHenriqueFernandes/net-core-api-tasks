using Microsoft.EntityFrameworkCore;
using Tasks.Data.Map;
using Tasks.Models;

namespace Tasks.Data
{
    public class TasksSystemDBContext : DbContext
    {
        public TasksSystemDBContext(DbContextOptions<TasksSystemDBContext> options)
            : base(options)
        {
            
        }

        public DbSet<UsuarioModel> Usuario { get; set; } 
        public DbSet<UsuarioModel> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TaskMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
