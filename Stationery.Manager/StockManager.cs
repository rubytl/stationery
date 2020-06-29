using Microsoft.EntityFrameworkCore;
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
    /// <seealso cref="Stationery.Manager.IStockManager" />
    public class StockManager : IStockManager
    {
        /// <summary>
        /// The site template repo
        /// </summary>
        private IEntityBaseRepository<Stock> stockRepo;

        private IEntityBaseRepository<Product> productRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockManager"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public StockManager(IUnitOfWork unitOfWork)
        {
            this.stockRepo = unitOfWork.GetRepository<Stock>();
            this.productRepo = unitOfWork.GetRepository<Product>();
        }

        /// <summary>
        /// Gets the site paging.
        /// </summary>
        /// <param name="pagingRequest">The paging request.</param>
        /// <returns></returns>
        public BaseModel<Stock> GetStocks()
        {
            var sites = this.stockRepo.GetAll().Include(s => s.Product);
            return new BaseModel<Stock>() { Models = sites, TotalCount = sites.Count() };
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> CreateNewStock(Stock model)
        {
            Product p = await this.productRepo.GetSingleAsync(s => s.Id == model.ProductId);
            if (p != null)
            {
                model.Product = p;
                await this.stockRepo.AddAsync(model);
                return await this.stockRepo.CommitAsync();
            }

            return 0;
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> UpdateStock(Stock model)
        {
            var template = await this.stockRepo.GetSingleAsync(s => s.Id == model.Id);
            template.Quantity = model.Quantity;
            template.Price = model.Price;
            return await this.stockRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> DeleteStocks(List<int> ids)
        {
            this.stockRepo.DeleteWhere(s => ids.Contains(s.Id));
            return await this.stockRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Stock> GetStock(int id)
        {
            return await this.stockRepo.GetSingleAsync(s => s.Id == id);
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> DeleteStock(int id)
        {
            this.stockRepo.DeleteWhere(s => s.Id == id);
            return await this.stockRepo.CommitAsync();
        }
    }
}
