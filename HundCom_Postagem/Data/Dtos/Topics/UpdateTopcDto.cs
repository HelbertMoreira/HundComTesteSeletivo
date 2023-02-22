using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Data.Dtos.Topics
{
    public class UpdateTopcDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome para o tópico é obrigatório")]
        public string Tema { get; set; }
    }
}
