using GameScheduler.BLL.Constants;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Models.UserModels
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        [Required]
        [MaxLength(12)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        public bool AsAdmin { get; set; }
    }
}
