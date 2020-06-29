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
    public class AppIdentityRoleClaimMapping : IEntityTypeConfiguration<AppIdentityRoleClaim>
    {
        public void Configure(EntityTypeBuilder<AppIdentityRoleClaim> builder)
        {
            builder.Property(t => t.ClaimType).HasMaxLength(20);
            builder.Property(t => t.ClaimValue).HasMaxLength(100);
            builder.Property(t => t.ClaimParameter).HasMaxLength(200);
        }
    }
}
