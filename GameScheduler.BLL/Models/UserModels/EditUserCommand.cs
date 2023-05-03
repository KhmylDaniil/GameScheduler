using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Models.UserModels
{
    public class EditUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool SwitchRole { get; set; }

        public string? SetNewPassword { get; set; }
    }
}
