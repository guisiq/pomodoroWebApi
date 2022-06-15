
using Microsoft.EntityFrameworkCore;

namespace pomodoro.data
{
      public class ApiContext : DbContext
    {
        public ApiContext ()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=LAPTOP-IC7APJAJ\SQLEXPRESS;Initial Catalog=avaliacaoA2p22;Integrated Security=True");
		}
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Meta>? Metas { get; set; }
        public DbSet<Pomodoro>? Pomodoros { get; set; }
        public DbSet<Tarefa>? Tarefas { get; set; }
        public DbSet<Rotina>? Rotinas { get; set; }
        public DbSet<TarefaRecursiva>? TarefaRecursivas { get; set; }
    }
}