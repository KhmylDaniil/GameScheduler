using MediatR;

namespace GameScheduler.BLL.Models.GameModels
{
    public class GetGamesQuery : IRequest<IEnumerable<GetGamesResponse>>
    {
        public string Name { get; set; }

        public DateTime MinDateTime { get; set; }

        public DateTime MaxDateTime { get; set; }

        public bool FilterByTime { get; set; }
    }
}
