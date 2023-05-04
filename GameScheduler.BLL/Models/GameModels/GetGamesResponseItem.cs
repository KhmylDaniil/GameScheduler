namespace GameScheduler.BLL.Models.GameModels
{
    public class GetGamesResponseItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }
    }
}