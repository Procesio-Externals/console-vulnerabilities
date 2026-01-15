// Microsoft.AspNetCore.Mvc.Core 2.2.5 is deprecated
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using VulnerableSolution;
using VulnerableSolution.MemoryLeaks.EventHandlers;
using VulnerableSolution.ThreadDeadlock;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VulnerableController : ControllerBase
{
    private readonly IThreadManager _threadManager;

    public VulnerableController(IThreadManager threadManager)
    {
        _threadManager = threadManager;
    }
   
    //starting a thread without managing it can lead to resource exhaustion
    //common vulnerability that can degrade system performance
    //or cause an application to crash when too many threads are left running
    [HttpGet("start-thread")]
    public IActionResult StartThread()
    {
        // Vulnerable: Creates a thread that is never properly managed or terminated
        Thread thread = new Thread(() =>
        {
            // Simulate a long-running task
            Thread.Sleep(10000);
            Console.WriteLine("Thread completed.");
        });

        thread.Start();

        return Ok("Thread started.");
    }

    //A deadlock occurs when two or more threads are waiting for each other to 
	//release resources, and none of them can proceed
    [HttpGet("deadlock")]
    public IActionResult CauseDeadlock()
    {
        // Start two threads
        Thread thread1 = new Thread(_threadManager.Thread1Task);
        Thread thread2 = new Thread(_threadManager.Thread2Task);

        thread1.Start();
        thread2.Start();

        thread1.Join(); // Wait for thread1 to complete
        thread2.Join(); // Wait for thread2 to complete

        return Ok("Both threads completed (if no deadlock occurred).");
    }
		
	private static void Thread1Work()
    {
        // Thread 1 acquires Lock A first
        lock (_lockA)
        {
            Console.WriteLine("Thread 1: Holding Lock A...");

            // Sleep ensures Thread 2 has time to acquire Lock B
            // This makes the deadlock deterministic for testing
            Thread.Sleep(1000); 

            Console.WriteLine("Thread 1: Waiting for Lock B...");
            
            // Thread 1 tries to acquire Lock B, but Thread 2 holds it
            lock (_lockB)
            {
                Console.WriteLine("Thread 1: Acquired Lock B.");
            }
        }
    }

    private static void Thread2Work()
    {
        // Thread 2 acquires Lock B first (The reverse order of Thread 1)
        lock (_lockB)
        {
            Console.WriteLine("Thread 2: Holding Lock B...");

            // Sleep ensures Thread 1 has time to acquire Lock A
            Thread.Sleep(1000);

            Console.WriteLine("Thread 2: Waiting for Lock A...");

            // Thread 2 tries to acquire Lock A, but Thread 1 holds it
            lock (_lockA)
            {
                Console.WriteLine("Thread 2: Acquired Lock A.");
            }
        }
    }
}

