namespace PSK.Model.BusinessEntities
{
    public class EmployeeRestriction
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int RestrictionId { get; set; }
        public Restriction Restriction { get; set; }
    }
}