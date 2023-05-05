using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Models.GameFileModels
{
    public class AddGameFileCommand : IRequest<Unit>
    {
        public Guid GameId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
