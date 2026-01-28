using Microsoft.AspNetCore.Mvc;
using VulnerableSolution.ThreadDeadlock;

namespace VulnerableSolution;

public class DeadLock
{
    private readonly IThreadManager _threadManager;

    public DeadLock(IThreadManager threadManager)
    {
        _threadManager = threadManager;
    }

    public bool StartThread()
    {
        // Vulnerable: Creates a thread that is never properly managed or terminated
        Thread thread = new Thread(() =>
        {
            // Simulate a long-running task
            Thread.Sleep(10000);
            Console.WriteLine("Thread completed.");
        });

        thread.Start();

        return true;    // Thread started.
    }

    public bool CauseDeadlock()
    {
        // Start two threads
        Thread thread1 = new Thread(_threadManager.Thread1Task);
        Thread thread2 = new Thread(_threadManager.Thread2Task);

        thread1.Start();
        thread2.Start();

        thread1.Join(); // Wait for thread1 to complete
        thread2.Join(); // Wait for thread2 to complete

        return true; // Both threads completed (if no deadlock occurred).
    }
}
