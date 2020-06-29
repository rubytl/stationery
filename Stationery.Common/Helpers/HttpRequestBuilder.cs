using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Stationery.Common.Helpers
{
    /// <summary>
    /// HttpRequestBuilder
    /// </summary>
    public class HttpRequestBuilder
    {
        /// <summary>
        /// The method
        /// </summary>
        private HttpMethod method = null;
        /// <summary>
        /// The request URI
        /// </summary>
        private string requestUri = "";
        /// <summary>
        /// The content
        /// </summary>
        private HttpContent content = null;
        /// <summary>
        /// The bearer token
        /// </summary>
        private string bearerToken = "";
        /// <summary>
        /// The accept header
        /// </summary>
        private string acceptHeader = "application/json";
        /// <summary>
        /// The database name
        /// </summary>
        private string dbName = "";
        /// <summary>
        /// The timeout
        /// </summary>
        private TimeSpan timeout = new TimeSpan(0, 5, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestBuilder"/> class.
        /// </summary>
        public HttpRequestBuilder()
        {
        }

        /// <summary>
        /// Adds the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        /// <summary>
        /// Adds the request URI.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        /// <summary>
        /// Adds the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        /// <summary>
        /// Adds the bearer token.
        /// </summary>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        /// <summary>
        /// Adds the name of the database.
        /// </summary>
        /// <param name="dbname">The dbname.</param>
        /// <returns></returns>
        public HttpRequestBuilder AddDbName(string dbname)
        {
            this.dbName = dbname;
            return this;
        }

        /// <summary>
        /// Adds the accept header.
        /// </summary>
        /// <param name="acceptHeader">The accept header.</param>
        /// <returns></returns>
        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        /// <summary>
        /// Adds the timeout.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync()
        {
            // Setup request
            var request = new HttpRequestMessage
            {
                Method = this.method,
                RequestUri = new Uri(this.requestUri)
            };

            if (this.content != null)
            {
                request.Content = this.content;
            }

            if (!string.IsNullOrEmpty(this.bearerToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.bearerToken);
            }

            if(!string.IsNullOrEmpty(this.dbName))
            {
                request.Headers.Add("dbName", this.dbName);
            }

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(this.acceptHeader))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.acceptHeader));
            }

            // Setup client
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = this.timeout;

                return await client.SendAsync(request);
            }
        }
    }
}
