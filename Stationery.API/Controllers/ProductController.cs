namespace Stationery.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Stationery.Common.Entities;
    using Stationery.Common.Helpers;
    using Stationery.Common.Models;
    using Stationery.Manager;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// SiteTemplateController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        #region Fields

        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IProductManager productManager;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController" /> class.
        /// </summary>
        /// <param name="siteTemplateManager">The site template manager.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ProductController(IProductManager productManager, ILoggerFactory loggerFactory)
        {
            this.productManager = productManager;
            this.logger = loggerFactory.CreateLogger("ProductController");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the templates.
        /// </summary>
        /// <param name="templateRequest">The template request.</param>
        /// <returns></returns>
        [HttpPost("products")]
        public IActionResult GetTemplates([FromBody]BaseRequest request)
        {
            try
            {
                return Ok(this.productManager.GetProducts());
            }
            catch (Exception ex)
            {
                Utilities.LogAppError(this.logger, ex);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("create")]
        public async Task<IActionResult> Create([FromBody]Product model)
        {
            try
            {
                return Ok(await this.productManager.CreateNewProduct(model));
            }
            catch (Exception ex)
            {
                this.logger.LogAppError(ex);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]Product model)
        {
            try
            {
                return Ok(await this.productManager.UpdateProduct(model));
            }
            catch (Exception ex)
            {
                this.logger.LogAppError(ex);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("delete")]
        public async Task<IActionResult> Delete([FromBody]List<int> ids)
        {
            try
            {
                return Ok(await this.productManager.DeleteProducts(ids));
            }
            catch (Exception ex)
            {
                this.logger.LogAppError(ex);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet("productById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                return Ok(await this.productManager.GetProduct(id));
            }
            catch (Exception ex)
            {
                this.logger.LogAppError(ex);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        #endregion Methods
    }
}