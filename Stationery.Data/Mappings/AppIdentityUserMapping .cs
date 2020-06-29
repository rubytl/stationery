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
    public class AppIdentityUserMapping : IEntityTypeConfiguration<AppIdentityUser>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUser> builder)
        {
            builder.Property(t => t.UserName).HasMaxLength(100);
            builder.Property(t => t.NormalizedUserName).HasMaxLength(100);
            builder.Property(t => t.Email).HasMaxLength(200);
            builder.Property(t => t.NormalizedEmail).HasMaxLength(200);
            builder.Property(t => t.PasswordHash).HasMaxLength(300);
            builder.Property(t => t.SecurityStamp).HasMaxLength(300);
            builder.Property(t => t.ConcurrencyStamp).HasMaxLength(300);
            builder.Property(t => t.PhoneNumber).HasMaxLength(200);
        }
    }
}
