using AUTOGLASS.ProductManager.Api;
using Microsoft.AspNetCore;

public class Program
{
    protected Program()
    {

    }
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}
