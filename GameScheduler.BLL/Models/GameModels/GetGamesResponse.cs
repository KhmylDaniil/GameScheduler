
namespace GameScheduler.BLL.Models.GameModels
{
    public class GetGamesResponse
    {
        public List<GetGamesResponseItem> Items { get; set; }

        public GetGamesQuery History { get; set; }

        public bool NextPageExist { get; set; }

        public bool PreviousPageExist { get; set; }
    }
}
