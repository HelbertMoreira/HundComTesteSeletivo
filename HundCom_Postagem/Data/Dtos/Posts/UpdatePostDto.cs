using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Data.Dtos.Posts
{
    public class UpdatePostDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título da postagem é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O conteúdo dapostagem é obrigatório")]
        public string Conteudo { get; set; }
    }
}
