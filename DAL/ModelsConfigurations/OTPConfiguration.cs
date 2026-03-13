using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ModelsConfigurations
{
    public class OTPConfiguration:IEntityTypeConfiguration<OTP>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OTP> builder)
        {
            builder.HasIndex(x=>x.Target).IsUnique();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Target).IsRequired();
            builder.Property(x => x.ExpirationTime).IsRequired();
            builder.Property(x => x.CodeHash).IsRequired();
            builder.Property(x => x.TargetType).IsRequired();
        }
    }
}
