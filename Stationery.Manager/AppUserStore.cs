using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stationery.Common.Entities;
using Stationery.Data;

namespace Stationery.Manager
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore{Stationery.Common.Entities.AppIdentityUser}" />
    public class AppUserStore : UserStore<AppIdentityUser, AppIdentityRole, MonitorDbContext, int,
         AppIdentityUserClaim, AppIdentityUserRole, AppIdentityUserLogin, AppIdentityUserToken, AppIdentityRoleClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AppUserStore(MonitorDbContext context)
            : base(context)
        {
        }
    }
}
