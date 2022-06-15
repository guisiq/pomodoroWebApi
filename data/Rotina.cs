using System.ComponentModel.DataAnnotations;
namespace pomodoro.data
{
    public class Rotina:Tarefa
    {
        private Rotina(){

        }
        public Rotina(Meta meta, string descricao = "", bool isConcluida = false) : base(meta, descricao, isConcluida)
        {
        }

        // could not be mapped because it is of type 'TimeOnly', which is not a supported primitive type or a valid entity type.
        public DateTime Inicio{ get; set; }
        public TimeSpan intervaloMaximo { get; set; }

    }
}