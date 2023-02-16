using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Models.Entities
{
    public class Postagem
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPostagem { get; set; }

        //public User Author { get; set; }
        //public int AuthorId { get; set; }

        public virtual Topico Topico { get; set; }
        public int TopicoId { get; set; }
        public virtual List<Comentario> Comentarios { get; set; }

    }
}
