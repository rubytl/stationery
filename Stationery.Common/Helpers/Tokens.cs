namespace Stationery.Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class Tokens
    {
        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static string RefreshToken(string token)
        {
            return token.Replace("{", string.Empty)
                .Replace("}", string.Empty);
        }
    }
}
