namespace GameScheduler.BLL.Models.GameModels
{
    public class GetGamesResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }
    }
}