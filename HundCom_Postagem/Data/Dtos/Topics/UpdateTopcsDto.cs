using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Data.Dtos.Topics
{
    public class UpdateTopcsDto
    {
        [Required(ErrorMessage = "O nome para o tópio é obrigatório")]
        public string Tema { get; set; }
    }
}
