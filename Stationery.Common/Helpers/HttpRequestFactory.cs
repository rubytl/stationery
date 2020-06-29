using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stationery.Common.Helpers
{
    public static class HttpRequestFactory
    {
        public static async Task<HttpResponseMessage> Get(string requestUri, string auth_token = "", string dbName = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(auth_token)
                                .AddDbName(dbName);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(
           string requestUri, object value, string auth_token = "", string dbName = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(auth_token)
                                .AddDbName(dbName)
                                .AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(
           string requestUri, object value, string auth_token = "", string dbName = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(auth_token)
                                .AddDbName(dbName)
                                .AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(
           string requestUri, object value, string auth_token = "", string dbName = "")
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddBearerToken(auth_token)
                                .AddDbName(dbName)
                                .AddContent(new PatchContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(string requestUri, object value, string auth_token, string dbName)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri + value)
                                .AddBearerToken(auth_token)
                                .AddDbName(dbName);

            return await builder.SendAsync();
        }
    }
}
