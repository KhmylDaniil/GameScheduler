using GameScheduler.BLL.Constants;
using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Entities
{
    public class User : EntityBase
    {
        protected User() { }

        public User(string name, string passwordHash, RoleType roleType)
        {
            Name = name;
            PasswordHash = passwordHash;
            RoleType = roleType;
            Games = new();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<Game> Games { get; set; }

        public RoleType RoleType { get; set; }
    }
}
