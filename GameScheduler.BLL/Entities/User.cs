using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Entities
{
    public class User : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public List<Game> Games { get; set; }
    }
}
