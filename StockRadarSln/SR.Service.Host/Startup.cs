using Common.Constants;
using Common.Helpers;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using SR.Service.Contract.ServiceContract;

namespace SR.Service.Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelServices()
                    .AddServiceModelMetadata()
                    .AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseServiceModel(builder =>
            {
                builder.AddService<StockNotificationService>(serviceOptions =>
                {
                    serviceOptions.BaseAddresses.Add(new Uri($"{SRConstants.Scheme_HTTP}://{SRConstants.HostAddress}/{SRConstants.ServicePath_StockNotificationService}"));
                    serviceOptions.BaseAddresses.Add(new Uri($"{SRConstants.Scheme_HTTPS}://{SRConstants.HostAddress}/{SRConstants.ServicePath_StockNotificationService}"));
                })

                //.AddServiceEndpoint<StockNotificationService, IStockNotificationService>(new BasicHttpBinding(), "/basichttp")

                // Add NetTcpBinding
                .AddServiceEndpoint<StockNotificationService, IStockNotificationService>(new NetTcpBinding(), SRHelper.GetServiceEndpoint(SRConstants.Scheme_NETTCP, SRConstants.HostAddress, SRConstants.NETTCP_PORT, SRConstants.ServicePath_StockNotificationService));


                // Configure WSDL to be available over http & https
                var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<CoreWCF.Description.ServiceMetadataBehavior>();
                serviceMetadataBehavior.HttpGetEnabled = serviceMetadataBehavior.HttpsGetEnabled = true;
            });
        }
    }
}
