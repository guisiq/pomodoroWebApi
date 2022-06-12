using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pomodoro.data
{
    public class Meta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public long MetasId { get; set; }

        public ICollection<Tarefa>? Tarefas { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}