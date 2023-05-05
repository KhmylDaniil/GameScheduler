using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Models.GameFileModels
{
    public class DeleteGameFileCommand : IRequest<Unit>
    {
        public Guid GameId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
