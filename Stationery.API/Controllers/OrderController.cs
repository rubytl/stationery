namespace Stationery.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Stationery.Common.Entities;
    using Stationery.Common.Helpers;
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
    public class OrderController : Controller
    {
        #region Fields

        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IOrderManager orderManager;

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
        public OrderController(IOrderManager orderManager, ILoggerFactory loggerFactory)
        {
            this.orderManager = orderManager;
            this.logger = loggerFactory.CreateLogger("OrderController");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the templates.
        /// </summary>
        /// <param name="templateRequest">The template request.</param>
        /// <returns></returns>
        [HttpPost("orders")]
        public IActionResult GetOrders()
        {
            try
            {
                return Ok(this.orderManager.GetOrders());
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
        public async Task<IActionResult> Create([FromBody]Order model)
        {
            try
            {
                return Ok(await this.orderManager.CreateNewOrder(model));
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
        public async Task<IActionResult> Update([FromBody]Order model)
        {
            try
            {
                return Ok(await this.orderManager.UpdateOrder(model));
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
                return Ok(await this.orderManager.DeleteOrders(ids));
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
        [HttpGet("orderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                return Ok(await this.orderManager.GetOrder(id));
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