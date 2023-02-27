using AutoMapper;
using HundCom_Postagem.Data.Dtos.Topics;
using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Profiles
{
    public class TopicoProfile : Profile
    {
        public TopicoProfile() 
        {
            CreateMap<CreateTopcDto, Topico>()
                .ForMember(dest => dest.Tema, m => m.MapFrom(src => src.Name)); 
            
            CreateMap<Topico, ReadTopcDto>()
                .ForMember(dest => dest.ListaPostagem, m => m.MapFrom(src => src.ListaPostagens))
                .ForMember(dest => dest.Tema, m => m.MapFrom(src => src.Tema));
            CreateMap<ReadTopcDto, Topico>();
            CreateMap<UpdateTopcDto, Topico>();

            CreateMap<IQueryable<Topico>, IQueryable<ReadTopcDto>>();
                
        }
    }
}
