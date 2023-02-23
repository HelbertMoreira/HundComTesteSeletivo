using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Models.Entities
{
    public class Comentario
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O conteúdo do comentário está vazio é obrigatório")]
        [MaxLength(150, ErrorMessage = "O contúdo da mensagem não pode ter mais que 150 caracteres")]
        public string Conteudo { get; set; }
        public DateTime DataComentario { get; set; }
        public virtual Postagem Postagem { get; set; }
        public int PostagemId { get; set; }
    }
}
