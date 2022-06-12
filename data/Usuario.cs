using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pomodoro.data
{
    public class Usuario
    {
        private Usuario(){

        }
        public Usuario(string nome, string login, string senha,string role)
        {
            this.Nome = nome;
            this.Login = login;
            this.Senha = senha;
            this.Role = role;

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public List<Meta> Metas;


    }
}