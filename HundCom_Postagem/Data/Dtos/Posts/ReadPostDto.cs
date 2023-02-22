using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Data.Dtos.Posts
{
    public class ReadPostDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Author { get; set; }
        public DateTime DataPostagem { get; set; }
        public Topico Topico { get; set; }
        public int TopicoId { get; set; }
        public virtual List<Comentario> Comentarios { get; set; }
    }
}
