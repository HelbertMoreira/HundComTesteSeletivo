using AutoMapper;
using HundCom_Postagem.Data.Dtos.Comments;
using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Profiles
{
    public class ComentarioProfile : Profile
    {
        public ComentarioProfile()
        {
            CreateMap<CreateCommentDto, Comentario>();
            CreateMap<Comentario, ReadCommentDto>().ReverseMap();
            //CreateMap<List<Comentario>, List<ReadCommentDto>>().ReverseMap();
            CreateMap<UpdateCommentDto, Comentario>();
        }
    }
}
