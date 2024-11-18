namespace VulnerableSolution.MemoryLeaks.EventHandlers
{
    public class Publisher
    {
        public event EventHandler DataChanged;

        public void RaiseEvent()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
