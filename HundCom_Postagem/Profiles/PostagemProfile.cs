using AutoMapper;
using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Data.Dtos.Topics;
using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Profiles
{
    public class PostagemProfile : Profile
    {
        public PostagemProfile() 
        {
            CreateMap<CreatePostDto, Postagem>();
            CreateMap<UpdatePostDto, Postagem>();
            CreateMap<Postagem, ReadPostDto>();
                //.ForMember(dest => dest.Titulo, m => m.MapFrom(src => src.Titulo))
                //.ForMember(dest => dest.Author, m => m.MapFrom(src => src.Autor))
                //.ForMember(dest => dest.Conteudo, m => m.MapFrom(src => src.Conteudo))
                //.ForMember(dest => dest.Topico, m => m.MapFrom(src => src.Topico))
                //.ForMember(dest => dest.Comentarios, m => m.MapFrom(src => src.Comentarios))
                //.ForMember(dest => dest.DataPostagem, m => m.MapFrom(src => src.DataPostagem));
        }
    }
}
