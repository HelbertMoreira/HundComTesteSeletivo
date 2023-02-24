using HundCom_Postagem.Data.Dtos.Posts;
using System.Text.Json.Serialization;

namespace HundCom_Postagem.Data.Dtos.Topics
{
    public class ReadTopcDto
    {
        
        public int Id { get; set; }
        public string? Tema { get; set; }
        public string? Autor { get; set; }
        public string AutorRole { get; set; }

        [JsonIgnore]
        public List<ReadPostDto>? ListaPostagem { get; set; }

    }
}
