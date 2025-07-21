using Common.Constants;
using CoreWCF.Configuration;
using SR.Service.Host;
using System.Diagnostics;

/// <summary>
/// Hosts the StockRadar Service as a console application.
/// </summary>
/// <remarks>This program initializes and runs a web host configured to listen on HTTP, HTTPS, and NetTcp ports.</remarks>
internal class HostProgram
{
    private static void Main(string[] args)
    {
        Console.WriteLine("This is a console app which will host the StockRadar Service.");

        var host = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options => {
                //options.ListenAnyIP(SRConstants.HTTP_PORT);
                //options.ListenAnyIP(SRConstants.HTTPS_PORT, listenOptions =>
                //{
                //    listenOptions.UseHttps();
                //    if (Debugger.IsAttached)
                //    {
                //        listenOptions.UseConnectionLogging();
                //    }
                //});
            })
            .UseNetTcp(SRConstants.NETTCP_PORT)
            .UseStartup<Startup>();

        host.Build().Run();
    }
}