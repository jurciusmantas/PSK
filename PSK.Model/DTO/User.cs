namespace PSK.Model.DTO
{
    public class User
    {
        public Employee Employee { get; set; }
        public string Token { get; set; }
        public string ExpiredAt { get; set; }

    }
}
