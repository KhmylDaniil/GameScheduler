using MediatR;

namespace GameScheduler.BLL.Models.UserModels
{
    public class LoginUserCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
