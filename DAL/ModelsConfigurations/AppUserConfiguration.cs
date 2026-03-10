using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ModelsConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            #region PhoneNumber
            builder.Property(u => u.PhoneNumber).HasMaxLength(11).IsRequired();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            #endregion
        }
    }
}
