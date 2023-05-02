using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GameScheduler.BLL.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Текущий айди пользователя
        /// </summary>
        public Guid CurrentUserId
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    return Guid.Empty;

                var value = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value
                    ?? throw new ApplicationSystemBaseException("Айди текущего пользователя не определено");

                return new Guid(value);
            }
        }

        public string Role
        {
            get
            {
                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    return Constants.SystemRoles.GuestRoleName;

                var value = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                    ?? throw new ApplicationSystemBaseException("Роль текущего пользователя не определена");

                return new string(value);
            }
        }
    }
}
