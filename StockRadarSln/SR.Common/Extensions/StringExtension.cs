namespace SR.Common.Extensions
{
    /// <summary>
    /// Extension class for string operations.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Determines whether the specified string is null or an empty string.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns><see langword="true"/> if the string is <see langword="null"/> or an empty string (""); otherwise, <see
        /// langword="false"/>.</returns>
        public static bool IsNullOrEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
