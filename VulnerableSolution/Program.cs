using System.Security.Cryptography;
using System.Text;
using VulnerableSolution.EndlessLoop;
using VulnerableSolution.InjectionLoop;

// Hardcoded sensitive information
string ApiSecret = "SuperSecretAPIKey";

Encrypt(ApiSecret);
Encrypt("easyKey");

// Start an endless loop
StartEndlessLoop();


// start an endless loop thorugh dependency injection
StartDependencyInjectionEndlessLoop(args);

static void Encrypt(string apiSecret)
{
    Console.WriteLine("Insecure Application Running");

    // Weak cryptographic practice: Using MD5 for hashing
    using (MD5 md5 = MD5.Create())
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(apiSecret);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        Console.WriteLine("MD5 Hash: " + BitConverter.ToString(hashBytes).Replace("-", ""));
    }
}

static void StartEndlessLoop()
{
    var loopCode = new LoopManager();
    loopCode.UseLoopBll();
}

static void StartDependencyInjectionEndlessLoop(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    //Add the service
    builder.Services.AddScoped<IInjectionLoopManager, InjectionLoopManager>();
    builder.Services.AddScoped<IInjectionLoopBll, InjectionLoopBll>();

    var app = builder.Build();
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        var myDependency = services.GetRequiredService<IInjectionLoopManager>();
    }

    app.Run();
}