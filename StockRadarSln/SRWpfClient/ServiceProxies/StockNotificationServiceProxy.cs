using SR.Service.Contract.ServiceContract;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfCore.Proxies;

namespace SR.WpfClient.ServiceProxies
{
    /// <summary>
    /// Provides a proxy for interacting with the stock notification service, enabling communication between a client
    /// and the service using a specified callback instance, binding, and remote address.
    /// </summary>
    /// <remarks>This class is intended for internal use and facilitates the creation of a service proxy for
    /// stock notifications. It inherits from <see cref="ServiceProxyBase{TService, TCallback}"/> to provide base
    /// functionality for service communication.</remarks>
    internal class StockNotificationServiceProxy : ServiceProxyBase<IStockNotificationService, IStockNotificationCallback>
    {
        public StockNotificationServiceProxy(IStockNotificationCallback callbackInstance, Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress)
        {
        }
    }
}
