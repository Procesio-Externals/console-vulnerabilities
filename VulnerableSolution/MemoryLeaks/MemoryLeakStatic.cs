namespace VulnerableSolution.MemoryLeaks
{
    //Static fields keep objects alive for the lifetime of the application.
    public class MemoryLeakStatic
    {
        public static List<string> LeakyList = new List<string>();

        public void AddData()
        {
            LeakyList.Add(new string('x', 1000));
        }
    }
}
