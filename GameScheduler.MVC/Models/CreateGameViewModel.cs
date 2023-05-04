using GameScheduler.BLL.Models.GameModels;

namespace GameScheduler.MVC.Models
{
    public class CreateGameViewModel : CreateGameCommand
    {
        public Dictionary<Guid, string> UsersList { get; set; }
    }
}
