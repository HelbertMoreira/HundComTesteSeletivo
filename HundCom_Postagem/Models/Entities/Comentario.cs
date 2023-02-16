namespace HundCom_Postagem.Models.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }

        //public User User { get; set; }
        //public int UserId { get; set; }

        public DateTime DataComentario { get; set; }

        public virtual Postagem Postagem { get; set; }
        public int PostagemId { get; set; }
    }
}
