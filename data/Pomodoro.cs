using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pomodoro.data
{
    public class Pomodoro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PomodoroId { get; set; }
        
        public long Intervalo { get; set;}
        public DateTime data { get; set; }
        public TipoPomodoro tipoPomodoro { get; set; }
    }
}