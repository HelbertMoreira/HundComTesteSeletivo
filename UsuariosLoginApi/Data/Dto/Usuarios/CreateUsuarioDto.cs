using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UsuariosLoginApi.Data.Dto.Usuarios
{
    public class CreateUsuarioDto
    {
        [Required]
        [DisplayName("Nome de Usuário")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirmar senha")]
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
