namespace Stationery.Manager
{
    using Stationery.Common.Entities;
    using Stationery.Common.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// ISiteTemplateManager
    /// </summary>
    public interface IStockManager
    {
        #region Methods

        /// <summary>
        /// Creates the new Stock.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> CreateNewStock(Stock model);

        /// <summary>
        /// Gets products
        /// </summary>
        /// <param name="templateRequest">The Stock request.</param>
        /// <returns></returns>
        BaseModel<Stock> GetStocks();

        /// <summary>
        /// Updates the Stock.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> UpdateStock(Stock model);

        /// <summary>
        /// Deletes the templates.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        Task<int> DeleteStocks(List<int> ids);

        /// <summary>
        /// Gets the Stock.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Stock> GetStock(int id);

        /// <summary>
        /// Gets the Stock.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> DeleteStock(int id);

        #endregion Methods
    }
}