using MediatR;

namespace GameScheduler.BLL.Models.GameModels
{
    public class DeleteGameCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
