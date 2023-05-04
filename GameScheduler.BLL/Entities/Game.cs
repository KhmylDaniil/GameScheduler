using System;
using System.ComponentModel.DataAnnotations;

namespace GameScheduler.BLL.Entities
{
    public class Game : EntityBase
    {
        protected Game() { }

        public Game(string name, string description, DateTime dateTime, List<User> users)
        {
            Name = name;
            Description = description;
            DateTime = dateTime;
            Users = users;
        }

        [Required]
        public string Name { get; set; } 

        public string? Description { get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public List<User> Users { get; set; } = new();

        internal void ChangeGame(string name, string description, DateTime dateTime, List<User> users)
        {
            Name = name;
            Description = description;
            DateTime = dateTime;
            Users = users;
        }
    }
}
