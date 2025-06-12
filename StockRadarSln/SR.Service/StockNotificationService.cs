using SR.Service.Contract.DataContract;
using SR.Service.Contract.ServiceContract;
using System.ServiceModel;

namespace SR.Service
{
    /// <summary>
    /// Provides functionality for managing stock notifications, including retrieving stock details, subscribing to
    /// stock updates, and notifying clients of stock price changes.
    /// </summary>
    public class StockNotificationService : IStockNotificationService
    {
        private readonly Dictionary<IStockNotificationCallback, HashSet<string>> _clientVsSymbolsSubscription = new Dictionary<IStockNotificationCallback, HashSet<string>>();
        private IList<StockData> _stockDataList = new List<StockData>();

        private Timer _stockUpdatePublisherTimer;
        private int _charAEquivalentInteger = 65;
        private int _stockPriceToggleThreshold = 10;
        private int _stockPriceMinValue = 0;
        private int _stockPriceMaxValue = 100;

        public StockNotificationService()
        {
            _stockUpdatePublisherTimer = new Timer(StockUpdatePublisher, null, 1000, 1000);
        }

        public IList<StockData> GetStockDetails()
        {
            _stockDataList.Clear();
            var random = new Random();
            for (int i = 0; i < 26; i++)
            {
                var charEquivalent = Convert.ToChar(_charAEquivalentInteger + i);
                _stockDataList.Add(new StockData()
                {
                    CompanyName = $"{charEquivalent}{charEquivalent}{charEquivalent}",
                    CurrentPrice = random.Next(1, 100),
                    Volume = random.Next(1, 100),
                    Symbol = $"{charEquivalent}",
                });
            }

            return _stockDataList;
        }

        public void SubscribeToStockUpdates(IList<string> symbols)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IStockNotificationCallback>();

            lock (_clientVsSymbolsSubscription)
            {
                if (!_clientVsSymbolsSubscription.ContainsKey(callback))
                {
                    _clientVsSymbolsSubscription.Add(callback, new HashSet<string>());
                }
            }

            foreach (var symbol in symbols)
            {
                if (_clientVsSymbolsSubscription[callback].Contains(symbol))
                {
                    continue;
                }
                _clientVsSymbolsSubscription[callback].Add(symbol);
            }
        }

        public void UnsubscribeFromStockUpdates(IList<string> symbols)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IStockNotificationCallback>();

            lock (_clientVsSymbolsSubscription)
            {
                if (!_clientVsSymbolsSubscription.ContainsKey(callback))
                {
                    return;
                }
            }

            foreach (var symbol in symbols)
            {
                if (_clientVsSymbolsSubscription[callback].Contains(symbol))
                {
                    _clientVsSymbolsSubscription[callback].Remove(symbol);
                }
            }

            if (_clientVsSymbolsSubscription[callback].Count == 0)
            {
                _clientVsSymbolsSubscription.Remove(callback);
            }
        }

        private void StockUpdatePublisher(object? state)
        {
            lock (_clientVsSymbolsSubscription) 
            {
                foreach (var clientVsSymbols in _clientVsSymbolsSubscription)
                {
                    var clientCallback = clientVsSymbols.Key;
                    var clientSymbols = clientVsSymbols.Value;
                    foreach (var symbol in clientSymbols)
                    {
                        var updatedStock = GetUpdatedStock(symbol);
                        if(updatedStock != null)
                        {
                            clientCallback.OnStockUpdated(updatedStock);
                        }
                    }
                }
            }
        }

        private StockData GetUpdatedStock(string symbol)
        {
            var stockBySymbol = _stockDataList.Where(x => x.Symbol.Equals(symbol)).FirstOrDefault();
            if(stockBySymbol != null)
            {
                int currentPrice = (int)stockBySymbol.CurrentPrice;
                int minValue = currentPrice - _stockPriceToggleThreshold;
                int maxValue = currentPrice + _stockPriceToggleThreshold;

                if(minValue < _stockPriceMinValue)
                {
                    minValue = _stockPriceMinValue;
                }
                if(maxValue > _stockPriceMaxValue)
                {
                    maxValue = _stockPriceMaxValue;
                }

                stockBySymbol.CurrentPrice = new Random().Next(minValue, maxValue);
                stockBySymbol.LastUpdatedDateTime = DateTime.UtcNow;
            }

            return stockBySymbol;
        }
    }
}
