using GameScheduler.BLL.Constants;
using GameScheduler.BLL.Entities;

namespace GameScheduler.BLL.Models.UserModels
{
    public class GetUserByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public RoleType RoleType { get; set; }

        public List<Game> Games { get; set; }
    }
}
