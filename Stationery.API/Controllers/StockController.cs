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
    public class StockController : Controller
    {
        #region Fields

        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IStockManager stockManager;

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
        public StockController(IStockManager stockManager, ILoggerFactory loggerFactory)
        {
            this.stockManager = stockManager;
            this.logger = loggerFactory.CreateLogger("StockController");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the templates.
        /// </summary>
        /// <param name="templateRequest">The template request.</param>
        /// <returns></returns>
        [HttpPost("stocks")]
        public IActionResult GetStocks([FromBody] StockRequest request)
        {
            try
            {
                return Ok(this.stockManager.GetStocks());
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
        public async Task<IActionResult> Create([FromBody]Stock model)
        {
            try
            {
                return Ok(await this.stockManager.CreateNewStock(model));
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
        public async Task<IActionResult> Update([FromBody]Stock model)
        {
            try
            {
                return Ok(await this.stockManager.UpdateStock(model));
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
                return Ok(await this.stockManager.DeleteStocks(ids));
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
        [HttpGet("stockById/{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            try
            {
                return Ok(await this.stockManager.GetStock(id));
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