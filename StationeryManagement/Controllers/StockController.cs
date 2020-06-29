namespace Stationery.UI.Controllers
{
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

    /// <summary>
    /// StockController
    /// </summary>
    /// <seealso cref="Stationery.UI.Controllers.CommonController" />
    [Authorize]
    public class StockController : CommonController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">The configuration.</param>
        public StockController(IOptions<StationeryAPIOptions> options, IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(options, httpContextAccessor, loggerFactory, configuration)
        {
            this.logger = loggerFactory.CreateLogger("StockController");
        }

        #endregion Constructors

        #region Methods

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
            StockRequest request = new StockRequest()
            {
                Page = new PagingRequest() { PageIndex = page - 1, PageSize = pageSize }
            };

            try
            {
                StockListViewModel model = null;
                if (!string.IsNullOrEmpty(parametersSearch))
                {
                    request = JsonConvert.DeserializeObject<StockRequest>(parametersSearch);
                }

                request.Page = new PagingRequest() { PageIndex = page - 1, PageSize = pageSize };
                var result = await this.PostAsync<BaseModel<Stock>>(HttpUriFactory.GetStocksRequest(this.options.Value.APIUrl), request);
                if (result != null)
                {
                    int pageCount = (result.TotalCount + pageSize - 1) / pageSize;
                    var nextPage = page == pageCount ? page : (page + 1);
                    var previousPage = page > 1 ? (page - 1) : page;
                    model = this.CreateDefaultVM<StockListViewModel, StockRequest>(request, parametersSearch, page, pageSize, nextPage, previousPage, pageCount, "Stock", "Index");
                    model.Templates = result.Models;
                }
                else
                {
                    model = this.CreateDefaultVM<StockListViewModel, StockRequest>(request, parametersSearch, page, pageSize, 1, 1, 1, "Stock", "Index");
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
                return View(this.CreateDefaultVM<StockListViewModel, StockRequest>(request, parametersSearch, page, pageSize, 1, 1, 1, "Stock", "Index"));
            }
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            StockRegisterViewModel model = new StockRegisterViewModel()
            {
                Products = GetProducts()
            };

            return View(model);
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(StockRegisterViewModel model, string productSearchString)
        {
            try
            {
                if (string.IsNullOrEmpty(productSearchString))
                {
                    ModelState.AddModelError("Products", "Please select a product");
                    model.Products = this.GetProducts();
                    return View(model);
                }

                Stock s = new Stock()
                {
                    Quantity = model.Quantity,
                    Price = model.Price,
                    ProductId = int.Parse(productSearchString)
                };

                var recordAdded = await this.PutAsync<int>(HttpUriFactory.GetNewStockRequest(this.options.Value.APIUrl), s);
                if (recordAdded > 0)
                {
                    ViewBag.Message = "Added successful";
                }
                else
                {
                    ViewBag.Message = TempData["Message"];
                }

                model.Products = this.GetProducts();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return View(model);
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await this.GetAsync<Stock>(HttpUriFactory.GetStockByIdRequest(this.options.Value.APIUrl, id));
                if (model == null)
                {
                    ViewBag.Message = TempData["Message"];
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return View(null);
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Stock model)
        {
            try
            {
                var result = await this.PutAsync<int>(HttpUriFactory.GetUpdateStockRequest(this.options.Value.APIUrl), model);
                if (result > 0)
                {
                    ViewBag.Message = "Saved successful";
                }
                else
                {
                    ViewBag.Message = TempData["Message"];
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return View(null);
            }
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteTemplate(string templateIds)
        {
            try
            {
                if (string.IsNullOrEmpty(templateIds))
                {
                    TempData["Message"] = "Please select templates to delete";
                    return RedirectToAction("Index");
                }

                var deletedStatus = await this.PutAsync<int>(HttpUriFactory.GetDeleteStockRequest(this.options.Value.APIUrl), templateIds.Split(","));
                if (deletedStatus > 0)
                {
                    TempData["Message"] = "Deleted successful";
                }
                else
                {
                    TempData["Message"] = TempData["Message"];
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetProducts()
        {
            var result = this.PostAsync<BaseModel<Product>>(HttpUriFactory.GetProductsRequest(this.options.Value.APIUrl), new BaseRequest()).Result;
            if (result != null)
            {
                return result.Models.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                });
            }

            return null;
        }

        #endregion Methods
    }
}