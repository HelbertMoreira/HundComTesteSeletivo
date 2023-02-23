using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosLoginApi.Data.Dto.Usuarios;
using UsuariosLoginApi.Models;

namespace UsuariosLoginApi.Profiles.Usuarios

{
    public class UsuarioProfile : Profile
    {

        public UsuarioProfile() 
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
        }
    }
}
