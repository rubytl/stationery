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
    public class AppIdentityRoleMapping : IEntityTypeConfiguration<AppIdentityRole>
    {
        public void Configure(EntityTypeBuilder<AppIdentityRole> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(200);
            builder.Property(t => t.NormalizedName).HasMaxLength(200);
            builder.Property(t => t.ConcurrencyStamp).HasMaxLength(200);
        }
    }
}
