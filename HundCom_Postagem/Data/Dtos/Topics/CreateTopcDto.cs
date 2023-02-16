using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Data.Dtos.Topics
{
    public class CreateTopcDto
    {
        [Key]
        [Required(ErrorMessage = "O nome para o tópio é obrigatório")]
        public string Name { get; set; }
    }
}
