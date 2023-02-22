using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HundCom_Postagem.Models.Entities
{
    public class Topico
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome para o tópico é obrigatório")]
        public string? Tema { get; set; }

        public virtual List<Postagem>? ListaPostagens { get; set; }
    }
}
