namespace Stationery.Manager
{
    using Stationery.Common.Entities;
    using Stationery.Common.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// ISiteTemplateManager
    /// </summary>
    public interface IOrderManager
    {
        #region Methods

        /// <summary>
        /// Creates the new Order.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> CreateNewOrder(Order model);

        /// <summary>
        /// Gets products
        /// </summary>
        /// <param name="templateRequest">The Order request.</param>
        /// <returns></returns>
        BaseModel<Order> GetOrders();

        /// <summary>
        /// Updates the Order.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> UpdateOrder(Order model);

        /// <summary>
        /// Deletes the templates.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        Task<int> DeleteOrders(List<int> ids);

        /// <summary>
        /// Gets the Order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Order> GetOrder(int id);
        #endregion Methods
    }
}