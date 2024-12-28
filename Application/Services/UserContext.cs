using Domain.Abstractions.Base;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class UserContext : IUserContext
    {
        public UserContext(IHttpContextAccessor contextAccessor)
        {
            UserName = contextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "default";
            UserId = Convert.ToInt64(contextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value ?? "0");
        }
        public long UserId {  get; private set; }

        public string UserName { get; private set; }
    }
}
