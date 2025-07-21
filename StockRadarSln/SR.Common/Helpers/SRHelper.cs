namespace Common.Helpers
{
    public static class SRHelper
    {
        public static string GetServiceEndpoint(string scheme, string host, int port, string servicePath)
        {
            if (string.IsNullOrWhiteSpace(scheme) || string.IsNullOrWhiteSpace(host) || port <= 0 || string.IsNullOrWhiteSpace(servicePath))
            {
                throw new ArgumentException("Invalid parameters for constructing service endpoint.");
            }

            return $"{scheme}://{host}:{port}/{servicePath}";
        }
    }
}
