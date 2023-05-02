using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
