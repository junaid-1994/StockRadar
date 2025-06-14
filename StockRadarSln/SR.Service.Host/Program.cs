using CoreWCF;
using CoreWCF.Configuration;
using SR.Service;
using SR.Service.Contract.ServiceContract;

/// <summary>
/// Represents the entry point of the StockRadar Service console application.
/// </summary>
/// <remarks>This application hosts the StockRadar Service using a WCF service model. It configures the necessary
/// services, endpoints, and bindings, and starts the application to listen for incoming requests.</remarks>
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("This is a console app which will host the StockRadar Service.");

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddServiceModelServices();
        builder.Services.AddServiceModelMetadata();

        var app = builder.Build();
        app.UseServiceModel(builder => 
        {
            builder.AddService<StockNotificationService>();
            builder.AddServiceEndpoint<StockNotificationService, IStockNotificationService>(
                new BasicHttpBinding(), "/StockNotificationService");
        });

        app.Run();

        var endpointAddress = OperationContext.Current.EndpointDispatcher.EndpointAddress;
        Console.WriteLine($"Service is listening at: {endpointAddress.Uri}");
        Console.ReadKey();
    }
}