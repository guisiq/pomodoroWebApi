using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pomodoro.data
{
    public class Pomodoro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PomodoroId { get; set; }
        public TimeSpan IntervaloReal { get; set;}
        public TimeSpan IntervaloProgramado { get; set;}
        public DateTime data { get; set; }
        public TipoPomodoro tipoPomodoro { get; set; }
    }
}