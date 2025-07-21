namespace Common.Constants
{
    /// <summary>
    /// Provides a collection of constants used for configuring service endpoints and paths.
    /// </summary>
    /// <remarks>This class contains predefined values for common network configurations, such as port
    /// numbers, host addresses, and service paths. These constants are intended to simplify the setup and usage of
    /// services by providing consistent, reusable values.</remarks>
    public class SRConstants
    {
        public const int HTTP_PORT = 50189;
        public const int HTTPS_PORT = 50190;
        public const int NETTCP_PORT = 8089;
        public const string Scheme_NETTCP = "net.tcp";
        public const string Scheme_HTTP = "http";
        public const string Scheme_HTTPS = "https";
        public const string HostAddress = "localhost";
        public const string ServicePath_StockNotificationService = "StockNotificationService";
    }
}
