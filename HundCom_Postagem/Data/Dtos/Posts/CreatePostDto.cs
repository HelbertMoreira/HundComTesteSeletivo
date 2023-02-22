using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Data.Dtos.Posts
{
    public class CreatePostDto
    {       
        public string Titulo { get; set; }
        public string Conteudo { get; set; }

        //public int AuthorId { get; set; }
        public int TopicoId { get; set; }
    }
}
