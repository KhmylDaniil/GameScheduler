using GameScheduler.BLL.Constants;

namespace GameScheduler.BLL.Abstractions
{
    public interface IUserContext
    {
        Guid CurrentUserId { get; }

        RoleType RoleType { get; }
    }
}
