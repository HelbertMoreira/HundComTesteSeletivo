using HundCom_Postagem.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HundCom_Postagem.Data.Dtos.Topics
{
    public class ReadTopcDto
    {
        
        public int Id { get; set; }

        [DisplayName("Topic")]
        public string Tema { get; set; }

        public Postagem? Postagem { get; set; }
    }
}
