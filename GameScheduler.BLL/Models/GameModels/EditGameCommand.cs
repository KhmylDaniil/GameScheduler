using GameScheduler.BLL.Entities;
using MediatR;

namespace GameScheduler.BLL.Models.GameModels
{
    public class EditGameCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }

        public List<User> Users { get; set; }
    }
}
