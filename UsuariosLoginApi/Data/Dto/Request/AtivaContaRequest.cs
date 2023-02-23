using System.ComponentModel.DataAnnotations;

namespace UsuariosLoginApi.Data.Dto.Request
{
    public class AtivaContaRequest
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}
