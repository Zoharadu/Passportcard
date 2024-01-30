using passportcard.Interfaces;

namespace passportcard.Implementations
{
    public class LifePolicy : IPolicy
    {
        public PolicyType Type { get; set; } = new PolicyType();
        public string FullName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public bool IsSmoker { get; set; } = false;
        public decimal Amount { get; set; } = decimal.Zero;
    }
}
