using System.Text.Json;

namespace VulnerableSolution
{
    public class InsecureReflection
    {
        /// <summary>
        /// This method demonstrates the vulnerability pattern:
        /// 1. Untrusted string input defines the Type.
        /// 2. Reflection resolves that Type.
        /// 3. The serializer creates an instance of that arbitrary Type.
        /// </summary>
        public void ProcessDynamicPayload(string jsonPayload, string typeName)
        {
            try
            {
                // STEP 1: DANGEROUS REFLECTION
                // The tool should flag Type.GetType() when used with user-controlled input.
                // This allows an attacker to specify ANY available class in the assembly.
                Type targetType = Type.GetType(typeName);

                if (targetType == null)
                {
                    throw new Exception("Type could not be resolved.");
                }

                // STEP 2: INSECURE DESERIALIZATION
                // The tool should flag the use of a dynamic 'Type' object in a deserializer.
                // This instantiates the attacker-chosen class and populates it with data.
                object result = JsonSerializer.Deserialize(jsonPayload, targetType);

                Console.WriteLine($"Successfully deserialized instance of: {result.GetType().FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during processing: {ex.Message}");
            }
        }
    }
}
