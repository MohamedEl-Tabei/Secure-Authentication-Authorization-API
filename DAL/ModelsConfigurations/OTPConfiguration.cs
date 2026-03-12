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
            builder.HasKey(x => x.Target);
            builder.Property(x => x.IsVerified).HasDefaultValue(false).IsRequired();
            builder.Property(x => x.ExpirationTime).IsRequired();
            builder.Property(x => x.TargetType).IsRequired();
        }
    }
}
