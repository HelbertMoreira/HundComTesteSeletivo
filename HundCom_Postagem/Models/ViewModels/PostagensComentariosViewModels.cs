using HundCom_Postagem.Data.Dtos.Comments;
using HundCom_Postagem.Data.Dtos.Posts;

namespace HundCom_Postagem.Models.ViewModels
{
    public class PostagensComentariosViewModels
    {
        public List<ReadCommentDto> ComentarioLista { get; set; }
        public List<ReadPostDto> PostagemLista { get; set; }
        public ReadCommentDto Comentario { get; set; }
    }
}
