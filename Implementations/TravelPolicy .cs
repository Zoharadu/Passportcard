using passportcard.Interfaces;

namespace passportcard.Implementations
{
    public class TravelPolicy : IPolicy
    {
        public PolicyType Type { get; set; } = new PolicyType();
        public string FullName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string Country { get; set; } = "";
        public int Days { get; set; } = 0;
    }
}
