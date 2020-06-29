namespace Stationery.UI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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
    using System.Threading.Tasks;

    /// <summary>
    /// ProductController
    /// </summary>
    /// <seealso cref="Stationery.UI.Controllers.CommonController" />
    [Authorize]
    public class ProductController : CommonController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">The configuration.</param>
        public ProductController(IOptions<StationeryAPIOptions> options, IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(options, httpContextAccessor, loggerFactory, configuration)
        {
            this.logger = loggerFactory.CreateLogger("ProductController");
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
            BaseRequest request = new BaseRequest()
            {
                Page = new PagingRequest() { PageIndex = page - 1, PageSize = pageSize }
            };

            try
            {
                ProductListViewModel model = null;
                if (!string.IsNullOrEmpty(parametersSearch))
                {
                    request = JsonConvert.DeserializeObject<BaseRequest>(parametersSearch);
                }

                request.Page = new PagingRequest() { PageIndex = page - 1, PageSize = pageSize };
                var result = await this.PostAsync<BaseModel<Product>>(HttpUriFactory.GetProductsRequest(this.options.Value.APIUrl), request);
                if (result != null)
                {
                    int pageCount = (result.TotalCount + pageSize - 1) / pageSize;
                    var nextPage = page == pageCount ? page : (page + 1);
                    var previousPage = page > 1 ? (page - 1) : page;
                    model = this.CreateDefaultVM<ProductListViewModel, BaseRequest>(request, parametersSearch, page, pageSize, nextPage, previousPage, pageCount, "Product", "Index");
                    model.Templates = result.Models;
                }
                else
                {
                    model = this.CreateDefaultVM<ProductListViewModel, BaseRequest>(request, parametersSearch, page, pageSize, 1, 1, 1, "Product", "Index");
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
                return View(this.CreateDefaultVM<ProductListViewModel, BaseRequest>(request, parametersSearch, page, pageSize, 1, 1, 1, "Product", "Index"));
            }
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            Product model = new Product()
            {
            };

            return View(model);
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(Product model)
        {
            try
            {
                var recordAdded = await this.PutAsync<int>(HttpUriFactory.GetNewProductRequest(this.options.Value.APIUrl), model);
                if (recordAdded > 0)
                {
                    ViewBag.Message = "Added successful";
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
                var model = await this.GetAsync<Product>(HttpUriFactory.GetProductByIdRequest(this.options.Value.APIUrl, id));
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
        public async Task<IActionResult> Details(Product model)
        {
            try
            {
                var result = await this.PutAsync<int>(HttpUriFactory.GetUpdateProductRequest(this.options.Value.APIUrl), model);
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

                var deletedStatus = await this.PutAsync<int>(HttpUriFactory.GetDeleteProductRequest(this.options.Value.APIUrl), templateIds.Split(","));
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

        #endregion Methods
    }
}