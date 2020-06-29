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
    public class AppIdentityUserLoginMapping : IEntityTypeConfiguration<AppIdentityUserLogin>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUserLogin> builder)
        {
            builder.Property(t => t.ProviderDisplayName).HasMaxLength(100);
        }
    }
}
