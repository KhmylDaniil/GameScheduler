using GameScheduler.BLL.Entities;

namespace GameScheduler.BLL.Exceptions
{
    internal class EntityNotFoundException<T> : ApplicationSystemBaseException where T : EntityBase
    {
        public EntityNotFoundException(Guid id)
            : base($"Не найдена сущность {typeof(T)} с ИД {id}.")
        {
        }
    }
}
