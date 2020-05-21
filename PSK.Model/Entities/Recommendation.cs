namespace PSK.Model.Entities
{
    public class Recommendation
    {
        public int Id { set; get; }
        public int TopicId { set; get; }
        public Topic Topic { set; get; }
        public int ReceiverId {set; get; }
        public Employee Receiver { set; get; }
        public int CreatorId { set; get; }
        public Employee Creator { set; get; }
    }
}
