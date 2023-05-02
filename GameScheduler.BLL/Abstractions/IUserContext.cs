
namespace GameScheduler.BLL.Abstractions
{
    public interface IUserContext
    {
        Guid CurrentUserId { get; }

        string Role { get; }
    }
}
