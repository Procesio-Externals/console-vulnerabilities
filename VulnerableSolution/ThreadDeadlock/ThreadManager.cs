namespace VulnerableSolution.ThreadDeadlock
{
    public class ThreadManager : IThreadManager
    {
        private static readonly object _lock1 = new object();
        private static readonly object _lock2 = new object();

        public void Thread1Task()
        {
            lock (_lock1) //Even Visual Studio gives warning of lock statement with inconsistent order possibly forming a cycle
            {
                Console.WriteLine("Thread 1 acquired lock1");
                Thread.Sleep(100); // Simulate work

                lock (_lock2) // Wait for lock2
                {
                    Console.WriteLine("Thread 1 acquired lock2");
                }
            }
        }

        public void Thread2Task()
        {
            lock (_lock2)
            {
                Console.WriteLine("Thread 2 acquired lock2");
                Thread.Sleep(100); // Simulate work

                lock (_lock1) // Wait for lock1
                {
                    Console.WriteLine("Thread 2 acquired lock1");
                }
            }
        }
    }
}
