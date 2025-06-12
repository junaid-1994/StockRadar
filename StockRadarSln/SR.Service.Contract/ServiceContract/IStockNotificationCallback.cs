using SR.Service.Contract.DataContract;
using System.ServiceModel;

namespace SR.Service.Contract.ServiceContract
{
    /// <summary>
    /// Defines a callback contract for receiving stock update notifications.
    /// </summary>
    /// <remarks>This interface is intended to be implemented by clients that wish to receive real-time
    /// updates about stock changes. The callback method is invoked by the service when stock data is updated.</remarks>
    public interface IStockNotificationCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnStockUpdated(StockData data);
    }
}
