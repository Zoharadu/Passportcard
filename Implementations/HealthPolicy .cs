using passportcard.Interfaces;

namespace passportcard.Implementations
{
    public class HealthPolicy:IPolicy
    {
        public PolicyType Type { get; set; } = new PolicyType();
        public string FullName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string Gender { get; set; }= "";
        public decimal Deductible { get; set; }= decimal.Zero;
    }
}
