using System.Runtime.Serialization;

namespace SR.Service.Contract.DataContract
{
    /// <summary>
    /// Represents stock market data for a specific company, including pricing and trading information.
    /// </summary>
    /// <remarks>This class provides details about a stock, such as its symbol, company name, exchange, 
    /// current price, opening and closing prices, and trading volume. It is commonly used in  financial applications to
    /// track and analyze stock performance.</remarks>
    [DataContract]
    public class StockData
    {
        [DataMember]
        public string? Symbol { get; set; }

        [DataMember]
        public string? CompanyName { get; set; }

        [DataMember]
        public string? Exchange { get; set; }

        [DataMember]
        public decimal CurrentPrice { get; set; }

        [DataMember]
        public decimal OpenPrice { get; set; }

        [DataMember]
        public decimal ClosePrice { get; set; }

        [DataMember]
        public long Volume { get; set; }

        [DataMember]
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
