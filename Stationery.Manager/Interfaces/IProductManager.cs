namespace Stationery.Manager
{
    using Stationery.Common.Entities;
    using Stationery.Common.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// ISiteTemplateManager
    /// </summary>
    public interface IProductManager
    {
        #region Methods

        /// <summary>
        /// Creates the new Product.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> CreateNewProduct(Product model);

        /// <summary>
        /// Gets products
        /// </summary>
        /// <param name="templateRequest">The Product request.</param>
        /// <returns></returns>
        BaseModel<Product> GetProducts();

        /// <summary>
        /// Updates the Product.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<int> UpdateProduct(Product model);

        /// <summary>
        /// Deletes the templates.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        Task<int> DeleteProducts(List<int> ids);

        /// <summary>
        /// Gets the Product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Product> GetProduct(int id);
        #endregion Methods
    }
}