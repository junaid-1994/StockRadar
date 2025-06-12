using SR.Service.Contract.DataContract;
using System.ServiceModel;

namespace SR.Service.Contract.ServiceContract
{
    /// <summary>
    /// Defines a service for retrieving stock details and managing subscriptions to stock updates.
    /// </summary>
    /// <remarks>This service allows clients to retrieve stock information and subscribe or unsubscribe from
    /// real-time stock updates. The service uses a callback mechanism, defined by <see
    /// cref="IStockNotificationCallback"/>, to notify clients of stock updates.</remarks>
    [ServiceContract(CallbackContract = typeof(IStockNotificationCallback))]
    public interface IStockNotificationService
    {
        [OperationContract]
        IList<StockData> GetStockDetails();

        [OperationContract]
        void SubscribeToStockUpdates(IList<string> symbols);

        [OperationContract]
        void UnsubscribeFromStockUpdates(IList<string> symbols);
    }
}
