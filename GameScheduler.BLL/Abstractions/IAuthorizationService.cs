using GameScheduler.BLL.Constants;

namespace GameScheduler.BLL.Abstractions
{
    public interface IAuthorizationService
    {
        void AuthorizationCheck(RoleType roleType = default);
    }
}
