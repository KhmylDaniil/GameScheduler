using MediatR;

namespace GameScheduler.BLL.Models.GameModels
{
    public class GetGameByIdQuery : IRequest<GetGameByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
