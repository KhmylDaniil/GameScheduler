using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Entities
{
    public class Game : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public List<User> Users { get; set; }
    }
}
