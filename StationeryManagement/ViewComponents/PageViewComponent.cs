using Microsoft.AspNetCore.Mvc;
using Stationery.UI.ViewModels;
using System.Threading.Tasks;

namespace Stationery.UI.ViewComponents
{
    public class PageViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="arr">The arr.</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(PageViewModel model)
        {
            string MyView = "Default";
            return await Task.FromResult(View(MyView, model));
        }
    }
}
