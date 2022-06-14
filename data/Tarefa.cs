using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pomodoro.data
{
    public class Tarefa
    {
        protected Tarefa(){

        }
        public Tarefa(Meta meta ,string descricao = "",bool isConcluida =false )
        {
            this.Descricao = descricao;
            this.IsConcluida = isConcluida;
            this.Meta = meta;

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TarefaId { get; set; }
        public string? Descricao { get; set; }
        public bool IsConcluida { get; set; }
        public long MetaId { get; set; }
        public virtual Meta? Meta { get; set; }

        public virtual ICollection<Pomodoro>? Pomodoros { get; set; }
        public int PomodorosConcluidos
        {
            get { return Pomodoros?.ToList().Count ?? 0; }
        }
       

    }
}