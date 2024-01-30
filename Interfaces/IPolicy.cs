using passportcard.Implementations;

namespace passportcard.Interfaces
{
    public interface IPolicy
    {
        PolicyType Type { get; set; }
        string FullName { get; set; }
        DateTime DateOfBirth { get; set; }  
    }
}
