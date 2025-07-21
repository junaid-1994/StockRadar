using Common.Constants;
using Common.Helpers;
using SR.Service.Contract.CallbackHandlers;
using SR.Service.Contract.ServiceContract;
using SR.WpfClient.ServiceProxies;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SR.WpfClient.Factories
{
    internal static class ProxyFactory
    {
        private static StockNotificationServiceProxy _stockNotificationServiceProxy;
        private static IStockNotificationCallback _stockNotificationCallback;
        public static StockNotificationServiceProxy GetStockNotificationServiceProxy(string scheme)
        {
            if (_stockNotificationServiceProxy == null)
            {
                Binding binding = null;
                EndpointAddress address = null;
                switch (scheme)
                {
                    case SRConstants.Scheme_NETTCP:
                        binding = new NetTcpBinding();
                        address = new EndpointAddress(
                        SRHelper.GetServiceEndpoint(SRConstants.Scheme_NETTCP, SRConstants.HostAddress, SRConstants.NETTCP_PORT, SRConstants.ServicePath_StockNotificationService));
                        break;
                    default:
                        throw new ArgumentException($"Unsupported scheme: {scheme}");
                }

                _stockNotificationCallback = new StockNotificationCallbackHandler(); 
                _stockNotificationServiceProxy = new StockNotificationServiceProxy(
                    _stockNotificationCallback, binding, address);
            }

            return _stockNotificationServiceProxy;
        }
    }
}
