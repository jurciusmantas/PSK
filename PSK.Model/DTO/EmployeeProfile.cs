using System.Collections.Generic;

namespace PSK.Model.DTO
{
    public class EmployeeProfile
    {
        public string LeaderName { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Employee> Subordinates { get; set; }
    }
}
