namespace GameScheduler.BLL.Models.UserModels
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public string Role { get; set; }
    }
}
