using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HundCom_Postagem.Models.Entities
{
    public class Postagem
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título da postagem é obrigatório")]
        [MaxLength(150, ErrorMessage = "O título deve ter no máximo 25 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O conteúdo dapostagem é obrigatório")]
        [MaxLength(150, ErrorMessage = "São permitidos apenas comentários com no máximo 150 caracteres")]
        public string Conteudo { get; set; }

        public DateTime DataPostagem { get; set; }

        
        public virtual Topico Topico { get; set; }
        public int TopicoId { get; set; }
        public virtual List<Comentario> Comentarios { get; set; }


    }
}
