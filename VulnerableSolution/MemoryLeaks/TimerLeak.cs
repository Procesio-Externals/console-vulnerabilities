namespace VulnerableSolution.MemoryLeaks
{
    //Timers can prevent objects from being collected if they hold references to the callback.
    public class TimerLeak
    {
        private readonly System.Timers.Timer _timer;

        public TimerLeak()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (sender, e) => Console.WriteLine("Timer elapsed");
            _timer.Start();
        }
    }
}
