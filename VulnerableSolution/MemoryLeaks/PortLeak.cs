using System.Net;
using System.Net.Sockets;

namespace VulnerableSolution.MemoryLeaks
{
    //Keeping ports open unnecessarily in your app can lead to resource leaks, security vulnerabilities, and system instability.
    //E.g. Failing to release a network socket can cause app to run out of resources, potentially crashing it or preventing new connections.
    public class PortLeak
    {
        public void KeepPortsAlive()
        {
            for (int i = 0; i < 1000; i++)
            {
                // Open a socket and leave it without closing
                var listener = new TcpListener(IPAddress.Loopback, 0);
                listener.Start();

                Console.WriteLine($"Port {((IPEndPoint)listener.LocalEndpoint).Port} is open.");
            }
        }
    }
}
