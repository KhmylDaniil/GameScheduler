﻿using MediatR;

namespace GameScheduler.BLL.Models.GameModels
{
    public class EditGameCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public List<Guid> Users { get; set; }
    }
}
