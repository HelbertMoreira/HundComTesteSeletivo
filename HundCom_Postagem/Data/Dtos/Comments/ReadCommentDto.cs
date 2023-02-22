using HundCom_Postagem.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Data.Dtos.Comments
{
    public class ReadCommentDto
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataComentario { get; set; }
        public Postagem Postagem { get; set; }
        public int PostagemId { get; set; }
    }
}
