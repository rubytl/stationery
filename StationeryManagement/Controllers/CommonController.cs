using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stationery.Common;
using Stationery.Common.Entities;
using Stationery.Common.Enums;
using Stationery.Common.Helpers;
using Stationery.Common.Models;
using Stationery.UI.Helpers;
using Stationery.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Stationery.UI.Controllers
{
    /// <summary>
    /// CommonController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CommonController : Controller, ICommonController
    {
        /// <summary>
        /// The authentication token
        /// </summary>
        protected readonly string auth_token;

        /// <summary>
        /// The database name
        /// </summary>
        protected readonly string dbName;

        /// <summary>
        /// The logger
        /// </summary>
        protected ILogger logger;

        /// <summary>
        /// The options
        /// </summary>
        protected readonly IOptions<StationeryAPIOptions> options;

        /// <summary>
        /// The HTTP context
        /// </summary>
        protected readonly HttpContext httpContext;

        /// <summary>
        /// The database name
        /// </summary>
        protected readonly string tenantName;

        /// <summary>
        /// The configuration
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonController" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public CommonController(IOptions<StationeryAPIOptions> options, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.options = options;
            this.logger = loggerFactory.CreateLogger("CommonController");
            this.httpContext = httpContextAccessor.HttpContext;
            this.configuration = configuration;
            var token = this.httpContext.User.Claims.FirstOrDefault(s => s.Type == Constants.AuthTokenFieldName);
            if (token != null)
            {
                this.auth_token = token.Value;
            }

            var db = this.httpContext.User.Claims.FirstOrDefault(s => s.Type == Constants.DatabaseFieldName);
            if (db != null)
            {
                this.dbName = db.Value;
            }

            var tenant = this.httpContext.User.Claims.FirstOrDefault(s => s.Type == Constants.TenantFieldName);
            if (tenant != null)
            {
                this.tenantName = tenant.Value;
            }
        }

        /// <summary>
        /// Gets the item per pages.
        /// </summary>
        /// <value>
        /// The item per pages.
        /// </value>
        public List<SelectListItem> ItemPerPages
        {
            get
            {
                return PagingHelper.GetPageOptionsFromConfiguration(this.configuration);
            }
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string requestUri)
        {
            T result = default(T);
            try
            {
                HttpResponseMessage response = await HttpRequestFactory.Get(requestUri, this.auth_token, this.dbName);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    ViewBag.Message = error;
                    TempData["Message"] = error;
                    Utilities.LogAppInformation(this.logger, error);
                    return result;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CommonController", ex.Message);
                ViewBag.Message = ex.Message;
                TempData["Message"] = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return result;
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string requestUri, object value)
        {
            T result = default(T);
            try
            {
                HttpResponseMessage response = await HttpRequestFactory.Post(requestUri, value, this.auth_token, this.dbName);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    ViewBag.Message = error;
                    TempData["Message"] = error;
                    Utilities.LogAppInformation(this.logger, error);
                    return result;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CommonController", ex.Message);
                ViewBag.Message = ex.Message;
                TempData["Message"] = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return result;
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<T> DeleteAsync<T>(string requestUri, object value)
        {
            T result = default(T);
            try
            {
                HttpResponseMessage response = await HttpRequestFactory.Delete(requestUri, value, this.auth_token, this.dbName);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    ViewBag.Message = error;
                    TempData["Message"] = error;
                    Utilities.LogAppInformation(this.logger, error);
                    return result;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CommonController", ex.Message);
                ViewBag.Message = ex.Message;
                TempData["Message"] = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return result;
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string requestUri, object value)
        {
            T result = default(T);
            try
            {
                HttpResponseMessage response = await HttpRequestFactory.Put(requestUri, value, this.auth_token, this.dbName);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    ViewBag.Message = error;
                    TempData["Message"] = error;
                    Utilities.LogAppInformation(this.logger, error);
                    return result;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CommonController", ex.Message);
                ViewBag.Message = ex.Message;
                TempData["Message"] = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return result;
            }
        }

        protected T CreateDefaultVM<T, R>(R request, string parametersSearch,
           int page, int pageSize, int nextPage, int previousPage, int pageCount,
           string controllerName, string actionName)
           where R : BaseRequest
           where T : BaseViewModel<R>
        {
            var result = Activator.CreateInstance<T>();
            result.Request = request;
            result.Page = new PageViewModel()
            {
                Parameters = parametersSearch,
                PageOptions = this.ItemPerPages,
                CurrentPage = page,
                PageSize = pageSize,
                NextPage = nextPage,
                PreviousPage = previousPage,
                PageCount = pageCount,
                ControllerName = controllerName,
                ActionName = actionName
            };

            return result;
        }
    }
}
