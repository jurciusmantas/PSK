namespace PSK.Model.Entities
{
    public class EmployeeRestriction
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int RestrictionId { get; set; }
        public virtual Restriction Restriction { get; set; }
    }
}