namespace pomodoro.data
{
    public class TarefaRecursiva:Tarefa
    {
        // could not be mapped because it is of type 'TimeOnly', which is not a supported primitive type or a valid entity type.
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DiaRecursao diaRecursao;

        private TarefaRecursiva(){
            
        }
        public TarefaRecursiva(Meta meta, string descricao = "", bool isConcluida = false) : base(meta, descricao, isConcluida)
        {
            this.Meta = meta;
        }

        [Flags]
        public enum DiaRecursao:Int32
        {
            None = 0,
            SEGUNDA = 1,
            TERCA = 2,
            QUARTA = 4,
            QUINTA = 8,
            SEXTA = 16,
            SABADO = 32,
            DOMINGO = 64
        }
    }
}