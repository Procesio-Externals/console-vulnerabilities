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
