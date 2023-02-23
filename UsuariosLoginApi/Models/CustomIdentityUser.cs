using Microsoft.AspNetCore.Identity;

namespace UsuariosLoginApi.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }
    }
}
