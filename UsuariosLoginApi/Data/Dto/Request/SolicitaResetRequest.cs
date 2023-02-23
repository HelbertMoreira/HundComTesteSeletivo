using System.ComponentModel.DataAnnotations;

namespace UsuariosLoginApi.Data.Dto.Request
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
