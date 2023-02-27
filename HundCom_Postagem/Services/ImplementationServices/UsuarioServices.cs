using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace HundCom_Postagem.Services.ImplementationServices
{
    public class AuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string? Name => GetClaimsIdentity().FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        public string? Role => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;

        public IEnumerable<Claim> GetClaimsIdentity()
        {            
            return _accessor.HttpContext.User.Claims;
        }
    }
}
