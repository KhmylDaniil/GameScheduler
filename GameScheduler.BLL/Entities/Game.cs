using GameScheduler.BLL.Exceptions;
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
            CreateStorageFolder();
        }

        [Required]
        public string Name { get; set; } 

        public string? Description { get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public List<User> Users { get; set; } = new();

        public List<GameFile> GameFiles { get; set; } = new();

        internal void ChangeGame(string name, string description, DateTime dateTime, List<User> users)
        {
            EditStorageFolder(Name, name);

            Name = name;
            Description = description;
            DateTime = dateTime;
            Users = users;
        }

        private void CreateStorageFolder()
        {
            if (Directory.Exists($@"D:\\testStorage\{Name}"))
                throw new RequestValidationException("Папка для игры с таким названием уже есть.");

            Directory.CreateDirectory($@"D:\\testStorage\{Name}");
        }

        private void EditStorageFolder(string oldName, string newName)
        {
            if (Directory.Exists($@"D:\\testStorage\{newName}"))
                throw new RequestValidationException("Папка для игры с таким названием уже есть.");

            Directory.Move($@"D:\\testStorage\{oldName}", $@"D:\\testStorage\{newName}");
        }

        internal void DeleteStorageFolder()
        {
            Directory.Delete($@"D:\\testStorage\{Name}");
        }
    }
}
