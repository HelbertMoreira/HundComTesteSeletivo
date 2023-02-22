using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Data.Dtos.Comments
{
    public class CreateCommentDto
    {
        public string Conteudo { get; set; } 
        public DateTime DataComentario { get; set; }
        public List<ReadPostDto> Postagens { get; set; }
        public int postagemId { get; set; } 
    }
}
