using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using passportcard.Interfaces;

namespace passportcard.Implementations
{
    public class PolicyReader
    {
        public static JObject? ReadPolicyFromFile(string filePath)
        {
            try
            {
                string policyJson = File.ReadAllText(filePath);
                    new StringEnumConverter();

                return JObject.Parse(policyJson); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading policy from file: {ex.Message}");
                return null;
            }
        }
    }
}
