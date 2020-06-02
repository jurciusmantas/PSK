namespace PSK.Model.DTO
{
    public class Day
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool? Completed { get; set; }
    }
}