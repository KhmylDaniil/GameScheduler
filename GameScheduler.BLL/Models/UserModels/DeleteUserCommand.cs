using MediatR;

namespace GameScheduler.BLL.Models.UserModels
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
