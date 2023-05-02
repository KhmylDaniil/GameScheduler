using GameScheduler.BLL.Abstractions;

namespace GameScheduler.BLL.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; set; } = DateTime.UtcNow;
    }
}
