using MediatR;

namespace GameScheduler.BLL.Models.UserModels
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
