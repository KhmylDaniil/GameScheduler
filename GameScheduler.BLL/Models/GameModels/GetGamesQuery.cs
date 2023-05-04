using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Models.GameModels
{
    public class GetGamesQuery : IRequest<GetGamesResponse>
    {
        public string Name { get; set; }

        public DateTime MinDateTime { get; set; }

        public DateTime MaxDateTime { get; set; }

        public bool FilterByTime { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 5;

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
    }
}
