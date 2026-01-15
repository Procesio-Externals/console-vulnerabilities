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

    //A deadlock occurs when two or more threads are waiting for each other to release resources, and none of them can proceed
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

}
