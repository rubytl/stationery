using Stationery.Common.Entities;
using Stationery.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stationery.Manager
{
    /// <summary>
    /// SiteTemplateManager
    /// </summary>
    /// <seealso cref="Stationery.Manager.IOrderManager" />
    public class OrderManager : IOrderManager
    {
        /// <summary>
        /// The site template repo
        /// </summary>
        private IEntityBaseRepository<Order> productRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderManager"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public OrderManager(IUnitOfWork unitOfWork)
        {
            this.productRepo = unitOfWork.GetRepository<Order>();
        }

        /// <summary>
        /// Gets the site paging.
        /// </summary>
        /// <param name="pagingRequest">The paging request.</param>
        /// <returns></returns>
        public BaseModel<Order> GetOrders()
        {
            var sites = this.productRepo.GetAll();
            return new BaseModel<Order>() { Models = sites, TotalCount = sites.Count() };
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> CreateNewOrder(Order model)
        {
            await this.productRepo.AddAsync(model);
            return await this.productRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> UpdateOrder(Order model)
        {
            var template = await this.productRepo.GetSingleAsync(s => s.Id == model.Id);
            template.UpdatedBy = model.UpdatedBy;
            template.UpdatedDate = DateTime.Now;
            template.Status = model.Status;
            return await this.productRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<int> DeleteOrders(List<int> ids)
        {
            this.productRepo.DeleteWhere(s => ids.Contains(s.Id));
            return await this.productRepo.CommitAsync();
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<Order> GetOrder(int id)
        {
            return await this.productRepo.GetSingleAsync(s => s.Id == id);
        }
    }
}
