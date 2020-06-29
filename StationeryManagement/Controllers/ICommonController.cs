using System.Threading.Tasks;

namespace Stationery.UI.Controllers
{
    /// <summary>
    /// ICommonManager
    /// </summary>
    public interface ICommonController
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string requestUri);

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string requestUri, object value);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<T> DeleteAsync<T>(string requestUri, object value);

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<T> PutAsync<T>(string requestUri, object value);
    }
}
