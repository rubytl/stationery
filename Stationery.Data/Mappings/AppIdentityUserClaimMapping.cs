using Microsoft.AspNetCore.Identity;
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
    public class AppIdentityUserClaimMapping : IEntityTypeConfiguration<AppIdentityUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUserClaim> builder)
        {
            builder.Property(t => t.ClaimType).HasMaxLength(20);
            builder.Property(t => t.ClaimValue).HasMaxLength(100);
        }
    }
}
