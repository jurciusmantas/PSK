namespace PSK.Model.Entities
{
    public class RecommendationArgs
    {
        public int TopicId { get; set; }
        public string RecommendedTo { get; set; }
        public int CreatedById { get; set; }
    }
}
