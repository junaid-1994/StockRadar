using System.ServiceModel;
using System.ServiceModel.Channels;
    
namespace WcfCore.Proxies
{
    /// <summary>
    /// Provides a base class for creating duplex service proxies that enable communication between a client and a
    /// service.`
    /// </summary>
    /// <remarks>This class establishes a duplex communication channel between the client and the service
    /// using the specified callback instance, binding, and remote address. Derived classes can use the created channel
    /// to invoke service operations.</remarks>
    /// <typeparam name="TService">The type of the service interface that defines the contract for the service.</typeparam>
    /// <typeparam name="TCallback">The type of the callback interface that defines the contract for the client-side callback.</typeparam>
    public abstract class ServiceProxyBase<TService, TCallback> where TService : class where TCallback : class
    {
        private readonly DuplexChannelFactory<TService> _duplexChannelFactory;

        private TService _channel;

        public TService Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        public ServiceProxyBase(TCallback callbackInstance, Binding binding, EndpointAddress remoteAddress)
        {
            var context = new InstanceContext(callbackInstance);
            _duplexChannelFactory = new DuplexChannelFactory<TService>(context, binding, remoteAddress);
            _channel = _duplexChannelFactory.CreateChannel();
        }
    }
}
