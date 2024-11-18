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

    // SQL Injection Vulnerability: User input is directly concatenated into the SQL command
    [HttpGet("get-user")]
    public IActionResult GetUser(
        [FromHeader] string token, //username should be taken from token not body payload
        string username)
    {
        string connectionString = "YourConnectionStringHere";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            // Process data...
        }
        return Ok();
    }

    // library used considers default hash algorithm as insecure and outdated
    [HttpGet("encrypt")]
    public IActionResult Encrypt(string text)
    {
        return Ok(Encryption.Encrypt(text));
    }

    //opening file connection without closing it can lead to resource exhaustion
    //common type of resource leak vulnerability
    [HttpGet("read-file")]
    public IActionResult ReadFile(string fileName)
    {
        // Path validation is also missing, allowing potential path traversal attacks
        string filePath = Path.Combine("files", fileName);

        // Opens a file stream but does not close it
        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(fileStream);

        // Reads the file content
        string content = reader.ReadToEnd(); // File remains open even after reading

        // Returns the file content as plain text
        return Content(content, "text/plain");
    }

    //opening a database connection without closing it
    //common vulnerability that can lead to connection leaks
    [HttpGet("get-data")]
    public IActionResult GetData()
    {
        // Vulnerable: Database connection is not closed
        SqlConnection connection = new SqlConnection("your-connection-string-here");
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
        SqlDataReader reader = command.ExecuteReader();

        List<string> users = new List<string>();
        while (reader.Read())
        {
            users.Add(reader["Username"].ToString());
        }

        // Connection remains open here!
        return Ok(users);
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

    //GC pressure: when app generates many objects in memory & GC is frequently triggered to reclaim unused memory
    //it's bad because Excessive Short-lived objects flood the Gen 0 heap and fills it up quickly
    //and Large objects are allocated directly on the LOH, increasing memory fragmentation and triggering costly full GC cycles.
    [HttpGet("generate-objects")]
    public IActionResult GenerateObjects()
    {
        // Allocate many short-lived objects
        for (int i = 0; i < 100000; i++)
        {
            string temp = new string('x', 100); // Short-lived object
        }

        // Allocate a large object repeatedly
        for (int i = 0; i < 1000; i++)
        {
            byte[] largeArray = new byte[1024 * 1024]; // 1 MB array
        }

        return Ok("Objects generated.");
    }

    //Memory leak: when app holds onto memory that is no longer needed preventing GC from reclaiming the memory
    //One of the most common causes of memory leaks in C# is failing to unsubscribe event handlers
    [HttpGet("memory-leak")]
    public IActionResult MemoryLeak()
    {
        var publisher = new Publisher();

        // Subscriber is created and subscribes to the publisher's event
        var subscriber = new Subscriber(publisher);

        // The subscriber goes out of scope and should be garbage collected,
        // but the event subscription keeps it alive.
        subscriber = null;

        // Raise an event to demonstrate that the subscriber is still being called
        publisher.RaiseEvent();

        return Ok("Memory leak created.");
    }
}
