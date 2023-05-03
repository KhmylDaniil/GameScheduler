using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Constants;
using GameScheduler.BLL.Exceptions;

namespace GameScheduler.BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserContext _userContext;

        public AuthorizationService(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public void AuthorizationCheck(RoleType roleType = default)
        {
            if (!Enum.IsDefined(roleType))
                throw new ApplicationSystemBaseException("Неизвестная роль");

            if (roleType > _userContext.RoleType)
                throw new ApplicationSystemBaseException("Ошибка авторизации, у вас нет прав на проведение данной операции.");
        }
    }
}
