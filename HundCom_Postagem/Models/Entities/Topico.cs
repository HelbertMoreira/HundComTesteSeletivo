using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Models.Entities
{
    public class Topico
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome para o tópio é obrigatório")]
        public string Tema { get; set; }

        public virtual List<Postagem>? Postagens { get; set; }
    }
}
