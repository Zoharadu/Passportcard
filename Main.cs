using Newtonsoft.Json.Linq;
using passportcard.Implementations;

namespace passportcard
{
    public class Main
    {
        private Dictionary<PolicyType, Action<JObject>> ratingFunctions;
        private decimal Rating { get; set; }

        public Main()
        {
            ratingFunctions = new Dictionary<PolicyType, Action<JObject>>
            {
                 { PolicyType.Life, RateHealthPolicy},
                 { PolicyType.Travel, RateTravelPolicy },
                 { PolicyType.Health, RateHealthPolicy }
            };
        }

        public void Run()
        {
            Console.WriteLine("Starting...");

            JObject policy = PolicyReader.ReadPolicyFromFile("policy.json");
            if (policy == null)
            {
                Console.WriteLine("Error loading policy");
            }
            else
            {
                if (ratingFunctions.TryGetValue((PolicyType)policy.Type, out var ratingFunction))
                {
                    ratingFunction(policy);
                }
                else
                {
                    Console.WriteLine("Unknown policy type");
                }
            }
        }

        private void RateHealthPolicy(JObject policy)
        {
            HealthPolicy newPolicy = new HealthPolicy();

            newPolicy.FullName = policy["FullName"]?.ToObject<string>();
            newPolicy.DateOfBirth = policy["DateOfBirth"]?.ToObject<DateTime>() ?? DateTime.MinValue;
            newPolicy.Gender = policy["Gender"]?.ToObject<string>();
            newPolicy.Deductible = policy["Deductible"]?.ToObject<decimal>() ?? 0;

            if (String.IsNullOrEmpty(newPolicy.Gender))
            {
                Console.WriteLine("Health policy must specify Gender");
            }
            if (newPolicy.Gender == "Male")
            {
                if (newPolicy.Deductible < 500)
                {
                    Rating = 1000m;
                }
                Rating = 900m;
            }
            else
            {
                if (newPolicy.Deductible < 800)
                {
                    Rating = 1100m;
                }
                Rating = 1000m;
            }
            Console.WriteLine("The Rating:" + Rating);
        }

        private void RateTravelPolicy(JObject policy)
        {
            TravelPolicy newPolicy = new TravelPolicy();

            newPolicy.FullName = policy["FullName"]?.ToObject<string>();
            newPolicy.DateOfBirth = policy["DateOfBirth"]?.ToObject<DateTime>() ?? DateTime.MinValue;
            newPolicy.Country = policy["Country"]?.ToObject<string>();
            newPolicy.Days = policy["Days"]?.ToObject<int>() ?? 0;

            if (newPolicy.Days <= 0)
            {
                Console.WriteLine("Travel policy must specify Days.");
            }
            if (newPolicy.Days > 180)
            {
                Console.WriteLine("Travel policy cannot be more then 180 Days.");
            }
            if (String.IsNullOrEmpty(newPolicy.Country))
            {
                Console.WriteLine("Travel policy must specify country.");
            }
            Rating = newPolicy.Days * 2.5m;
            if (newPolicy.Country == "Italy")
            {
                Rating *= 3;
            }
            Console.WriteLine("The Rating:" + Rating);
        }

        private void RateLifePolicy(JObject  policy)
        {
            LifePolicy newPolicy = new LifePolicy();

            newPolicy.FullName = policy["FullName"]?.ToObject<string>();
            newPolicy.DateOfBirth = policy["DateOfBirth"]?.ToObject<DateTime>() ?? DateTime.MinValue;
            newPolicy.IsSmoker = policy["IsSmoker"]?.ToObject<bool>() ?? false;
            newPolicy.Amount = policy["Amount"]?.ToObject<decimal>() ?? 0;

            if (newPolicy.DateOfBirth == DateTime.MinValue)
            {
                Console.WriteLine("Life policy must include Date of Birth.");
                return;
            }
            if (newPolicy.DateOfBirth < DateTime.Today.AddYears(-100))
            {
                Console.WriteLine("Max eligible age for coverage is 100 years.");
                return;
            }
            if (newPolicy.Amount == 0)
            {
                Console.WriteLine("Life policy must include an Amount.");
                return;
            }

            int age = DateTime.Today.Year - newPolicy.DateOfBirth.Year;
            if (newPolicy.DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < newPolicy.DateOfBirth.Day ||
                DateTime.Today.Month < newPolicy.DateOfBirth.Month)
            {
                age--;
            }
            decimal baseRate = newPolicy.Amount * age / 200;
            if (newPolicy.IsSmoker)
            {
                Rating = baseRate * 2;
            }
            Rating = baseRate;

            Console.WriteLine("The Rating:" + Rating);
        }
    }
}
