namespace PSK.Model.Entities
{
    public class Recommendation
    {
        public int Id { set; get; }
        public int TopicId { set; get; }
        public virtual Topic Topic { set; get; }
        public int ReceiverId {set; get; }
        public virtual Employee Receiver { set; get; }
        public int CreatorId { set; get; }
        public virtual Employee Creator { set; get; }
    }
}
