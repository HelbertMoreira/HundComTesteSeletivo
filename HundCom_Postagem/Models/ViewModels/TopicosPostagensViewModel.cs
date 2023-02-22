using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Data.Dtos.Topics;

namespace HundCom_Postagem.Models.ViewModels
{
    public class TopicosPostagensViewModel
    {        
        public List<ReadTopcDto>? ListaTopicos { get; set; }
        public List<ReadPostDto>? ListaPostagens { get; set; }
        public ReadPostDto? Postagem { get; set; }
        public ReadTopcDto? Topico { get; set; }
    }
}
