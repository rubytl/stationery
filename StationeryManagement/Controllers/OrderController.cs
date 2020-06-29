using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stationery.Common;
using Stationery.Common.Entities;
using Stationery.Common.Enums;
using Stationery.Common.Helpers;
using Stationery.Common.Models;
using Stationery.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stationery.UI.Controllers
{
    public class OrderController : CommonController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">The configuration.</param>
        public OrderController(IOptions<StationeryAPIOptions> options, IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(options, httpContextAccessor, loggerFactory, configuration)
        {
            this.logger = loggerFactory.CreateLogger("OrderController");
        }

        #endregion Constructors

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="parametersSearch">The parameters search.</param>
        /// <param name="pageSizeString">The page size string.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string parametersSearch, string pageSizeString, int page)
        {
            int pageSize = 20;
            page = page > 0 ? page : 1;
            if (!string.IsNullOrEmpty(pageSizeString))
            {
                pageSize = int.Parse(pageSizeString);
            }
            OrderRequest request = new OrderRequest()
            {
                Page = new PagingRequest() { PageIndex = page - 1, PageSize = pageSize }
            };

            try
            {
                OrderListViewModel model = null;
                if (!string.IsNullOrEmpty(parametersSearch))
                {
                    request = JsonConvert.DeserializeObject<OrderRequest>(parametersSearch);
                }

                request.Page = new PagingRequest() { PageIndex = page - 1, PageSize = pageSize };
                var result = await this.PostAsync<BaseModel<Order>>(HttpUriFactory.GetOrdersRequest(this.options.Value.APIUrl), request);
                if (result != null)
                {
                    int pageCount = (result.TotalCount + pageSize - 1) / pageSize;
                    var nextPage = page == pageCount ? page : (page + 1);
                    var previousPage = page > 1 ? (page - 1) : page;
                    model = this.CreateDefaultVM<OrderListViewModel, OrderRequest>(request, parametersSearch, page, pageSize, nextPage, previousPage, pageCount, "Order", "Index");
                    model.Templates = result.Models;
                }
                else
                {
                    model = this.CreateDefaultVM<OrderListViewModel, OrderRequest>(request, parametersSearch, page, pageSize, 1, 1, 1, "Order", "Index");
                }

                if (TempData["Message"] != null)
                {
                    ViewBag.Message = TempData["Message"];
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return View(this.CreateDefaultVM<OrderListViewModel, OrderRequest>(request, parametersSearch, page, pageSize, 1, 1, 1, "Order", "Index"));
            }
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            OrderRegisterListViewModel model = new OrderRegisterListViewModel()
            {
            };

            return View(model);
        }
    }
}