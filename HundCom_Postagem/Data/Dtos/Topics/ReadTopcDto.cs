using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Models.Entities;
using System.ComponentModel;

namespace HundCom_Postagem.Data.Dtos.Topics
{
    public class ReadTopcDto
    {
        
        public int Id { get; set; }

        public string? Tema { get; set; }

        public List<ReadPostDto>? ListaPostagem { get; set; }

    }
}
