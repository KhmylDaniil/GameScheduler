using GameScheduler.BLL.Models.GameModels;

namespace GameScheduler.MVC.Models
{
    public class EditGameViewModel : EditGameCommand
    {
        public Dictionary<Guid, string> UsersList { get; set; }
    }
}
