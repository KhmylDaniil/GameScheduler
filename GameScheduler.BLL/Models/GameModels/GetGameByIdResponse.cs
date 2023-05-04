namespace GameScheduler.BLL.Models.GameModels
{
    public class GetGameByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public Dictionary<Guid, string> Users { get; set; }
    }
}