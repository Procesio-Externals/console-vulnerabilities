namespace VulnerableSolution.MemoryLeaks.EventHandlers
{
    public class Subscriber
    {
        public Subscriber(Publisher publisher)
        {
            publisher.DataChanged += OnDataChanged;
        }

        private void OnDataChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Event received");
        }
    }
}
