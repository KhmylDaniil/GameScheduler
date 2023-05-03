using MediatR;

namespace GameScheduler.BLL.Models.UserModels
{
    public class GetUserQuery : IRequest<IEnumerable<GetUserResponse>>
    {
        public string Name { get; set; }
    }
}
