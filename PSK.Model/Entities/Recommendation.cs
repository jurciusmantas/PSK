namespace PSK.Model.Entities
{
    public class Recommendation
    {
        public int Id { set; get; }
        public int TopicId { set; get; }
        public string TopicName { set; get; }
        public int ReceiverId { set; get; }
        public string ReceiverName { set; get; }
        public int CreatorId { set; get; }
        public string CreatorName { set; get; }
    }
}
