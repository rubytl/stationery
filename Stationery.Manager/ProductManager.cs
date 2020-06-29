using Stationery.Common.Entities;
using Stationery.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stationery.Manager
{
    /// <summary>
    /// SiteTemplateManager
    /// </summary>
    /// <seealso cref="Stationery.Manager.IProductManager" />
    public class ProductManager : IProductManager
    {
        /// <summary>
        /// The site template repo
        /// </summary>
        private IEntityBaseRepository<Product> productRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManager"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ProductManager(IUnitOfWork unitOfWork)
        {
            this.productRepo = unitOfWork.GetRepository<Product>();
        }

        /// <summary>
        /// Gets the site paging.
        /// </summary>
        /// <param name="pagingRequest">The paging request.</param>
        /// <returns></returns>
        public BaseModel<Product> GetProducts()
        {
            var sites = this.productRepo.GetAll();
            return new BaseModel<Product>() { Models = sites, TotalCount = sites.Count() };
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> CreateNewProduct(Product model)
        {
            await this.productRepo.AddAsync(model);
            return await this.productRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> UpdateProduct(Product model)
        {
            var template = await this.productRepo.GetSingleAsync(s => s.Id == model.Id);
            template.Name = model.Name;
            return await this.productRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> DeleteProducts(List<int> ids)
        {
            this.productRepo.DeleteWhere(s => ids.Contains(s.Id));
            return await this.productRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Product> GetProduct(int id)
        {
            return await this.productRepo.GetSingleAsync(s => s.Id == id);
        }
    }
}
