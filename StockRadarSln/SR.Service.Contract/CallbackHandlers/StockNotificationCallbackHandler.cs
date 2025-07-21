using SR.Service.Contract.DataContract;
using SR.Service.Contract.ServiceContract;

namespace SR.Service.Contract.CallbackHandlers
{
    public class StockNotificationCallbackHandler : IStockNotificationCallback
    {
        public void OnStockUpdated(StockData data)
        {
            throw new NotImplementedException();
        }
    }
}
