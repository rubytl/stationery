using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stationery.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stationery.Data.Mappings
{
    public class AppIdentityUserRoleMapping : IEntityTypeConfiguration<AppIdentityUserRole>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUserRole> builder)
        {
        }
    }
}
