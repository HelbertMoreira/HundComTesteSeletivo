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
            CreateMap<UpdateCommentDto, Comentario>();
            CreateMap<Comentario, ReadCommentDto>();
                //.ForMember(dest => dest.Conteudo, m => m.MapFrom(src => src.Conteudo))
                //.ForMember(dest => dest.Autor, m => m.MapFrom(src => src.Autor))
                //.ForMember(dest => dest.Conteudo, m => m.MapFrom(src => src.Conteudo))
                //.ForMember(dest => dest.DataComentario, m => m.MapFrom(src => src.DataComentario))
                //.ForMember(dest => dest.Postagem, m => m.MapFrom(src => src.Postagem))
                //.ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id));
        }
    }
}
